using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace InternalViewerLib
{
    public class ObjViewHelper : TreeViewItem, INotifyPropertyChanged
    {

        private string propertyName;
        public string PropertyName
        {
            get { return propertyName; }
            set { propertyName = value;
                propertyNameTb.Text = value;
                NotifyPropertyChanged("PropertyName"); }
        }

        private TextBlock propertyNameTb = new TextBlock() { Foreground = Brushes.Brown };
        private TextBlock typeTb = new TextBlock() { Foreground = Brushes.Blue };
        private TextBlock valueStringTb = new TextBlock() { Foreground = Brushes.Green };

        public static string WorkingProperty = "Working...";
        public static ObjViewHelper WorkingObject = GeneratePendingObject();
        private static ObjViewHelper GeneratePendingObject()
        {
            return new ObjViewHelper(WorkingProperty, null);
        }

        public Object Obj { get; set; }

        public ObjViewHelper(string propertyName, Object obj)
        {
            this.Obj = obj;
            this.PropertyName = propertyName;
            if (obj != null)
            {
                this.TypeName = obj.GetType().Name;
                this.TypeStringValue = String.Format("{0}", obj);
                
            }
            Header = new StackPanel() { Orientation = Orientation.Horizontal, Children = { propertyNameTb, typeTb, valueStringTb } };
            // Add working sub object in the collection.
            // When expand we should change the object to real collection.
            //children = new ObservableCollection<ObjViewHelper>() { WorkingObject };
            if (propertyName != WorkingProperty)
            {
                Items.Add(GeneratePendingObject());
            }
        }

        public string ObjectName { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged( this, new PropertyChangedEventArgs(property));
            }
        }

        public async Task UpdateChildren()
        {
            if (Obj == null) return;
            ObjViewHelper wo = Items[0] as ObjViewHelper;
            if (wo == null) return;
            if (wo.PropertyName != "Working...") return;

            await Task.Yield();

            var objType = Obj.GetType();

            foreach( PropertyInfo info in objType.GetProperties())
            {
                ObjViewHelper helper;
                try
                {
                    object val;
                    var p = info.GetMethod.GetParameters();
                    if (p.Length > 0) val = "ParameterRequired";
                    else val = info.GetValue(Obj);
                    helper = new ObjViewHelper(info.Name, val);
                }
                catch (Exception ex)
                {
                    helper = new ObjViewHelper(info.Name + "Error", ex);
                }
                Items.Add(helper);
            }

            foreach (FieldInfo fieldInfo in objType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance))
            {
                ObjViewHelper helper;
                try
                {
                    object val;
                    val = fieldInfo.GetValue(Obj);
                    helper = new ObjViewHelper(fieldInfo.Name, val);
                }
                catch (Exception ex)
                {
                    helper = new ObjViewHelper(fieldInfo.Name + "Error", ex);
                }
                Items.Add(helper);
            }


            Items.Remove(wo);
            return;
        }

        protected override async void OnExpanded(System.Windows.RoutedEventArgs e)
        {
            base.OnExpanded(e);
            await UpdateChildren();
        }

        private string typeName;
        public string TypeName
        {
            get { return typeName; }
            set { typeName = value;
            typeTb.Text = value;
            NotifyPropertyChanged("TypeName");}
        }

        private string typeStringValue;
        public string TypeStringValue
        {
            get { return typeStringValue; }
            set { typeStringValue = value;
            valueStringTb.Text = value;
            NotifyPropertyChanged("TypeStringValue");}
        }
    }
}

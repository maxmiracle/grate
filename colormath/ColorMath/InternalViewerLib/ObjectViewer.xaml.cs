using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InternalViewerLib
{
    /// <summary>
    /// Interaction logic for ObjectViewer.xaml
    /// </summary>
    public partial class ObjectViewer : UserControl
    {
        private ObservableCollection<ObjViewHelper> objects;

        public readonly Dictionary<Type, Func<Object, UIElement>> TypeViewerDic = new Dictionary<Type,Func<Object, UIElement>>();


        public void AddStandardViewer(Type type, Type viewerClass)
        {
            TypeViewerDic.Add(type, new Func<Object, UIElement>((Object obj) => 
            {
                var constructor = viewerClass.GetConstructor(new Type[] { type });
                UIElement ui = constructor.Invoke(new Object[] { obj }) as UIElement;
                return ui; 
            }));
        }

        public UIElement GetViewerForType(Object obj)
        {
            Func<Object, UIElement> getUI;
            UIElement control = null;
            Type type = obj.GetType();
            if (TypeViewerDic.TryGetValue(type, out getUI))
            {
                control = getUI(obj);
            }
            return control;
        }

        public ObjectViewer()
        {
            InitializeComponent();
            objects = new ObservableCollection<ObjViewHelper>();
            ObjectTree.ItemsSource = objects;
        }

        public void AddObject(string name, object objectContext)
        {
            objects.Add(new ObjViewHelper(name, objectContext));
        }
    }
}

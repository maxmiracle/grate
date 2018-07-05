using Microsoft.VisualStudio.TestTools;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetBase.View;
using Net.NetBase;
using System.Diagnostics;

namespace NetBase
{
    public class SqImgLayer : IEnumerable<SqRt>, IDisposable
    {
        protected SqRt[,] layer;


        public int Width { get { return layer.GetLength(0); } }

        public int Height { get { return layer.GetLength(1); } }

        private delegate void XYFunction(XY xy);

        public static IEnumerable<XY> xyGen( int width, int height)
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    XY val;
                    val.X = i; val.Y = j;
                    yield return val;
                }
            }
        }

        public SqImgLayer(Bitmap img) : this( img.Width, img.Height )
        {
            foreach (var xy in xyGen(Width, Height))
            {
                layer[xy.X, xy.Y].setInput(img.GetPixel(xy.X, xy.Y)); 
            }
        }

        public SqImgLayer(int width, int height)
        {
            layer = new SqRt[width, height];
            XYFunction v = (XY xy) => {layer[xy.X, xy.Y] = new SqRt(xy.X, xy.Y, this);};
            foreach( var item in xyGen(width, height))
            {
                v(item);
            }
            //Parallel.ForEach( xyGen(width, height), item => v(item));
            SetRels();
        }

        public SqRt this[int x, int y]
        {
            get
            {
                if ((x < 0) || (x >= Width) || (y < 0) || (y >= Height))
                {
                    throw new ApplicationException(String.Format("SqImgLayer: Index out of bounds: x={0}, y={1}, Bound=((0,{2}),(0,{3}))", x, y, Width, Height));
                }
                return layer[x,y];
            }
            set
            {
                layer[x,y] = value;
            }
        }

        protected void SetRels()
        {
            foreach (SqRt sqrt in layer)
            {
                sqrt.InitializeLayerRelations();
            }
        }

        public bool ValidateXY(int x, int y)
        {
            if ( (x>=0)&&(y>=0)&&(x<Width)&&(y<Height))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public IEnumerator<SqRt> GetEnumerator()
        {
            return new SqImgLayerEnumerator(this);
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return new SqImgLayerEnumerator(this);
        }

        public class SqImgLayerEnumerator : IEnumerator, IEnumerator<SqRt>, IDisposable
        {
            private SqImgLayer sqImgLayer;
            private IEnumerator<XY> iter;

            public SqImgLayerEnumerator(SqImgLayer sqImgLayer)
            {
                this.sqImgLayer = sqImgLayer;
                Reset();
            }

            object IEnumerator.Current
            {
                get
                {
                    return (object)(sqImgLayer[iter.Current.X, iter.Current.Y]);
                }
            }

            SqRt IEnumerator<SqRt>.Current
            {
                get
                {
                    return sqImgLayer[iter.Current.X, iter.Current.Y];
                }
            }

            public bool MoveNext()
            {
                return iter.MoveNext();
            }

            public void Reset()
            {
                iter = xyGen(sqImgLayer.Width, sqImgLayer.Height).GetEnumerator();
            }

            public void Dispose()
            {
                if (sqImgLayer != null)
                {
                    sqImgLayer.Dispose();
                }
            }
        }

        public void Dispose()
        {
            // Do nothing - no disposable resources
        }



        public static SqImgLayer GetLayer()
        {
            Bitmap bitmap = TestImages.GetTestImage(0);
            SqImgLayer layer = new SqImgLayer(bitmap);
            //layer.Experiment_15_12_2015();
            return layer;
        }

        public void CalcF()
        {
            foreach (var v in this)
            {
                v.CalcLiquidityDirection();
            }
        }

        public void CalcS()
        {
            foreach (var v in this)
            {
                v.NeighbourGainEasy1();
            }
        }


        /// <summary>
        /// Метод вычисляет группы элементов по следующему алгоритму:
        /// 1 - Находим список точек, для которых нет доминирующих точек ( LiquidityDirection = XY.Self )
        /// 2 - Для найденных точек устанавливаем свойство LiquidityGroupMaster, указывающее на них самих
        /// 3 - Далее стековый алгоритм, который для всех точек, над которыми доминирует данная точка устанавливает
        /// свойство LiquidityGroupMaster. Если точка в свою очередь тоже доминирует над другими точками,
        ///  то она добавляется в очередь.
        /// </summary>
        public void CreateLiquidityGroups()
        {
            Trace.WriteLine("Create Liquidity Groups method is started");
            var selfDirectionElements = this.Where((item) => item.LiquidityDirection == XY.XYSelf);
            Trace.WriteLine("Total number of elements: " + this.Count().ToString());
            Trace.WriteLine("Self direction elements:" + selfDirectionElements.Count().ToString());

            Queue<SqRt> masterElements = new Queue<SqRt>();
            // 1 step : set LiquidityGroupMaster for self elements
            foreach (var sqrt in selfDirectionElements)
            {
                sqrt.LiquidityGroupMaster = sqrt;
                masterElements.Enqueue(sqrt);
            }
            while ( masterElements.Count() > 0)
            {
                SqRt currentElement = masterElements.Dequeue();
                //Trace.WriteLine(String.Format("Master element: ({0}, {1}) with owner ({2}, {3})", currentElement.X, currentElement.Y, currentElement.LiquidityGroupMaster.X, currentElement.LiquidityGroupMaster.Y));
                foreach ( var inpXY in currentElement.LiquidityInputs)
                {
                    SqRt inpSqRt = this[currentElement.X + inpXY.X, currentElement.Y + inpXY.Y];
                    inpSqRt.LiquidityGroupMaster = currentElement.LiquidityGroupMaster;
                    masterElements.Enqueue(inpSqRt);
                }
            }
        }

        public void CalcOut()
        {
            foreach (var v in this)
            {
                v.CalcOutputColor();
            }
        }

        public void Experiment_15_12_2015()
        {
            CalcS();
            CalcF();
            CreateLiquidityGroups();
            CalcOut();
            Trace.WriteLine(String.Format("Liquidity Max: {0}", Liquidity.LiquidityMax));
        }

        public void Experiment_04_04_2016()
        {
            CalcS();
            CalcF();
            CreateLiquidityGroups();
            CalculateAllGroupsAndPrint();
            CalcOut();
            Trace.WriteLine(String.Format("Liquidity Max: {0}", Liquidity.LiquidityMax));
        }

        private void CalculateAllGroupsAndPrint()
        {
            Trace.WriteLine("Calculate all groups and print method");
            var q = from item in this
                    group item by item.LiquidityGroupMaster into MasterGroup
                    select MasterGroup;
            var groups = q.ToList();
            Trace.WriteLine(String.Format("Groups count: {0}",groups.Count()));
        }

        public ImgComposition Experiment_09_12_2016()
        {
            CalcS();
            CalcF();
            CreateLiquidityGroups();
            var q = from item in this
                    group item by item.LiquidityGroupMaster into MasterGroup
                    select MasterGroup;
            var groups = q.ToList();

            SqImgLayerWindow window = new SqImgLayerWindow(this);
            window.ShowDialog();

            ImgComposition imgComposition = new ImgComposition(groups);

            return imgComposition;
        }

        internal ImgComposition GenerateCompositionOverLiquidity(Func<SqRt, SqRt, bool> liquidityFunc)
        {
            foreach (var sqRt in this)
            {
                sqRt.LiquidityGroupMaster = sqRt;
                foreach ( var sqRtNel in sqRt.nels)
                {
                    if ( liquidityFunc(sqRt, sqRtNel.Value) )
                    {
                        if ( sqRtNel.Value.LiquidityGroupMaster == null)
                        {
                            sqRtNel.Value.LiquidityGroupMaster = sqRt.LiquidityGroupMaster;
                        }
                        else
                        {
                            var q1 = from item in this
                                    where item.LiquidityGroupMaster == sqRtNel.Value.LiquidityGroupMaster
                                    select item;
                            foreach (var it in q1)
                            {
                                it.LiquidityGroupMaster = sqRt.LiquidityGroupMaster;
                            }
                        }

                    }
                }
            }
            var q = from item in this
                    group item by item.LiquidityGroupMaster into MasterGroup
                    select MasterGroup;
            var groups = q.ToList();

            ImgComposition imgComposition = new ImgComposition(groups);

            return imgComposition;
        }


    }
}

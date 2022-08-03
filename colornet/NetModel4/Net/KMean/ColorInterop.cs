using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers;
using Net.KMean;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Net
{
    
    public class ColorInterop
    {
        private static Logger log = LogManager.GetCurrentClassLogger();

        private static MLContext mlContext = new MLContext();

        public static Color GetPixelColor(BitmapSource bitmap, int x, int y)
        {
            Color color;
            var bytesPerPixel = (bitmap.Format.BitsPerPixel + 7) / 8;
            var bytes = new byte[bytesPerPixel];
            var rect = new Int32Rect(x, y, 1, 1);

            bitmap.CopyPixels(rect, bytes, bytesPerPixel, 0);

            if (bitmap.Format == PixelFormats.Bgra32)
            {
                color = Color.FromArgb(bytes[3], bytes[2], bytes[1], bytes[0]);
            }
            else if (bitmap.Format == PixelFormats.Bgr32)
            {
                color = Color.FromRgb(bytes[2], bytes[1], bytes[0]);
            }
            // handle other required formats
            else
            {
                color = Colors.Black;
            }

            return color;
        }


        public static List<xyColor> GetXyColors(BitmapSource source)
        {
            if (source.Format != PixelFormats.Bgra32)
                source = new FormatConvertedBitmap(source, PixelFormats.Bgra32, null, 0);

            var width = source.PixelWidth;
            var height = source.PixelHeight;

            List<xyColor> list = new List<xyColor>();
            for (int x = 0; x<width; x++)
                for(int y = 0; y< height; y++)
                {
                    xyColor xyc = new xyColor();
                    Color color = GetPixelColor(source, x, y);
                    xyc.color = color;
                    xyc.x = x;
                    xyc.y = y;
                    list.Add(xyc);
                }
            log.Debug(() => $"Image size: {list.Count}");
            return list;
            
        }

        private static ClusteringPredictionTransformer<KMeansModelParameters> Train(MLContext mlContext, IDataView data, int numberOfClusters)
        {
            var pipeline = mlContext.Clustering.Trainers.KMeans(numberOfClusters: numberOfClusters);
            var model = pipeline.Fit(data);
            return model;
        }

        public static List<ColorStatistic> AnalyseColors(BitmapImage bitmap)
        {
            var pixels = ColorInterop.GetXyColors(bitmap);
            var pixelEntries = pixels.Select(item => Convert(item));
            var fullData = mlContext.Data.LoadFromEnumerable(pixelEntries);
            var model = Train(mlContext, fullData, 13);
            VBuffer<float>[] centroidsBuffer = default;
            model.Model.GetClusterCentroids(ref centroidsBuffer, out int k);
            var colors = centroidsBuffer.Select(centroid => { 
                var array = centroid.DenseValues().ToArray();
                return FromRgb(array[0], array[1], array[2]);
                    }).ToArray();
            var labels = mlContext.Data
                .CreateEnumerable<Prediction>(model.Transform(fullData), reuseRowObject: false)
                .ToArray();
            var count = labels.GroupBy(label => label.PredictedLabel)
                .Select(group => new ColorStatistic()
                {
                    Name = group.Key.ToString(),
                    Color = colors[group.Key - 1],
                    Count = group.Count()
                })
                .OrderBy(x => x.Name).ToList();
            var countAll = count.Sum(x => x.Count);
            count.ForEach(x => x.Percent = (double)x.Count / countAll);
            return count;
        }

        public static Color FromRgb(float r, float g, float b)
        {
            return Color.FromRgb((byte)(r * 255), (byte)(g * 255), (byte)(b * 255));
        }


        private static PixelEntry Convert(xyColor xyColor)
        {
            var color = xyColor.color;
            return new PixelEntry
            {
                Features = new[]
                {
                    (float)color.R / 255.0f,
                    (float)color.G / 255.0f,
                    (float)color.B / 255.0f
                }
            };
        }
    }
}

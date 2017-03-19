using Chart.Mvc.ComplexChart;
using System.Collections.Generic;
using T001.Logic;

namespace T001.Models
    {
    public class GraphLogicData
        {
        public static string fillColor = "rgba(0,0,225,0.8)";
        public static string strokeColor = null;//"rgba(0,0,100,1)";
        public static string pointColor = "rgba(100,0,200,1)";
        public static string pointStrokeColor = "##fff";
        private static string pointHighlightFill = "#fff";
        private static string pointHighlightStroke = "rgba(220,220,220,1)";

        public static IEnumerable<string> Labels_For_Days
            {
            get
                {
                return new[]
                           {
                               "Sunday",
                               "Monday",
                               "Tuesday",
                               "Wednesday",
                               "Thursday",
                               "Friday",
                               "Saturday"
                           };
                }
            }

        public static IEnumerable<ComplexDataset> Datasets
            {
            get
                {
                return new List<ComplexDataset>
                           {
                               new ComplexDataset
                                   {
                                       Data = GraphCalculation.data,
                                       Label = "Tweets by Day",
                                       FillColor=fillColor,
                                       StrokeColor=strokeColor,
                                       PointColor=pointColor,
                                       PointStrokeColor=pointStrokeColor,
                                       PointHighlightFill=pointHighlightFill,
                                       PointHighlightStroke=pointHighlightStroke
                                   }

                           };
                }
            }
        }
    }
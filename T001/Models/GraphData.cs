using Chart.Mvc.ComplexChart;
using System.Collections.Generic;

namespace T001.Models
    {
    public class GraphData
        {
        private string fillColor = "rgba(0,0,225,0.8)";
        private string strokeColor = null;//"rgba(0,0,100,1)";
        private string pointColor = "rgba(100,0,200,1)";
        private string pointStrokeColor = "##fff";
        private string pointHighlightFill = "#fff";
        private string pointHighlightStroke = "rgba(220,220,220,1)";

        public string LabelText;
        public List<double> Data;
        public List<string> Label;
        public List<ComplexDataset> Datasets;

        public GraphData()
            {
            LabelText = null;
            Data = new List<double>();
            Label = new List<string>();
            Datasets = new List<ComplexDataset>();
            }
        public void Run()
            {
            Datasets = new List<ComplexDataset>
                           {
                               new ComplexDataset
                                   {
                                       Data = Data,
                                       Label = LabelText,
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

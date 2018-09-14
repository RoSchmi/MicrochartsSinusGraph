// RoSchmi Vers. 1.0     14.09.2018
// Github: RoSchmi/https://github.com/RoSchmi/MicrochartsSinusGraph
// This example shows a sinus curve using Microcharts for SkiaSharp and Xamarin
// Tutorial: https://www.youtube.com/watch?v=tmymWdmf1y4
// Git Repsitory: https://github.com/aloisdeniel/Microcharts
// Related Nuget repositories: Microcharts, Microcharts.Forms, SkiaSharp.Views.Forms, Xamarin.Forms
// Used Nuget repositories: SkiSharp.Views.Forms(1.60.2); Xamarin.Forms(3.1.0.637273)

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using SkiaSharp;
using SkiaSharp.Views;
using System.Threading;
using MicrochartsDemo.Model;

namespace MicrochartsDemo
{
    public partial class MainPage : ContentPage
    {
        List<Measurements> MeasurementsList;

        List<Microcharts.Entry> nullLineEntries;

        List<Microcharts.Entry> entries; 
       
        public MainPage()
        {
            InitializeComponent();

            // Make Null-Line
            nullLineEntries = new List<Microcharts.Entry>();
            for (int i = 0; i < 144; i++)
            {
                nullLineEntries.Add(new Microcharts.Entry(0));
            }
            // Show Null-Line
            ChartNullLine.Chart = new Microcharts.LineChartTableGraphs { Entries = nullLineEntries, BackgroundColor = SKColors.Transparent, LineSize = 2, MaxValue = 1100, MinValue = -1100, LineMode = Microcharts.LineMode.Straight /*LineAreaAlpha = 0*/};

            // Make Line with Sinus Curve
            MeasurementsList = new List<Measurements>();

           int samplesPerDay = 144;  // every 10 min
           int sampleCount = 100;   // day not completely filled
            
           for (int i = 1; i <= sampleCount; i++)    // make Sinus Curve
           {
                MeasurementsList.Add(new Measurements() { SampleTime = DateTime.Today.AddMinutes(i * (1440 / (float)samplesPerDay)), Value = 1000 * (float)Math.Sin( Math.PI / 2.0 + (i * ((4 * Math.PI) / (float)samplesPerDay))) });
           }

            // Transform to Scale with 1440 Points per day (1 point every minute) using linear interpolation
            entries = MakeEntryList.SamplesToEntryList1440(MeasurementsList, SKColors.Violet, SKColors.LightGreen);
            
            // Show Curve
            Chart1.Chart = new Microcharts.LineChartTableGraphs { Entries = entries, BackgroundColor = SKColors.Transparent, LineSize = 5, MaxValue = 1100, MinValue = -1100, LineMode = Microcharts.LineMode.Straight, /*LineAreaAlpha = 0*/};          
        }
    }
}



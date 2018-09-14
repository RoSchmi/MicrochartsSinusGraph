using System;
using System.Collections.Generic;
using System.Text;
using MicrochartsDemo.Model;
using SkiaSharp;

namespace MicrochartsDemo
{
    public static class MakeEntryList
    {
        public static List<Microcharts.Entry> SamplesToEntryList1440(List<Measurements> pMeasurementsList, SKColor pLineColor, SKColor pHideColor)
        {
            List<Microcharts.Entry> entries = new List<Microcharts.Entry>();
            float lastEntryValue = 0;
            float thisEntryValue = 0;
            int lastEntryIndex = 0;
            int thisEntryIndex = 0;

            for (int i = 0; i < pMeasurementsList.Count; i++)
            {
                thisEntryValue = pMeasurementsList[i].Value;
                if (i == 0)
                {
                    lastEntryValue = thisEntryValue;    //  extrapolate Curve backwards with the first measured Value
                    thisEntryIndex = Convert.ToInt32((pMeasurementsList[i].SampleTime - DateTime.Today).TotalMinutes);
                    for (int indexPtr = 0; indexPtr < thisEntryIndex; indexPtr++)
                    {
                        entries.Add(new Microcharts.Entry(lastEntryValue) { Color = pHideColor });  // Normally: SkColors.Transparent 
                    }
                }
                else
                {
                    thisEntryIndex = Convert.ToInt32((pMeasurementsList[i].SampleTime - DateTime.Today).TotalMinutes);

                    float increment = (thisEntryValue - lastEntryValue) / (float)(thisEntryIndex - lastEntryIndex);
                    for (int indexPtr = lastEntryIndex; indexPtr < thisEntryIndex; indexPtr++)
                    {
                        entries.Add(new Microcharts.Entry(lastEntryValue + (indexPtr - lastEntryIndex) * increment) { Color = pLineColor });
                    }
                    if (i == pMeasurementsList.Count - 1)
                    {
                        Console.WriteLine("ThisEntryIndex+1: " + (thisEntryIndex + 1) + "   pWantedEntryCount: " + 1440);
                        for (int indexPtr = thisEntryIndex + 1; indexPtr < 1440; indexPtr++)
                        {
                            entries.Add(new Microcharts.Entry(thisEntryValue) { Color = pHideColor });  // Normally: SkColors.Transparent 
                        }
                    }
                }
                lastEntryIndex = thisEntryIndex;
                lastEntryValue = pMeasurementsList[i].Value;
            }
            return entries;
        }

 

    }
}

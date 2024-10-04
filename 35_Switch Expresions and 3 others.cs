using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code
{
    public class WeatherStation
    {
        private Reading reading;
        private List<DateTime> recordDates = new List<DateTime>();
        private List<decimal> temperatures = new List<decimal>();

        public void AcceptReading(Reading reading)
        {
            this.reading = reading;
            recordDates.Add(DateTime.Now);
            temperatures.Add(reading.Temperature);
        }

        public void ClearAll()
        {
            reading = new Reading();
            recordDates.Clear();
            temperatures.Clear();
        }

        public decimal LatestTemperature => reading.Temperature;

        public decimal LatestPressure => reading.Pressure;

        public decimal LatestRainfall => reading.Rainfall;

        public bool HasHistory => recordDates.Count > 1? true: false;

        public Outlook ShortTermOutlook => reading.Temperature switch
        {
            _ when reading.Equals(new Reading()) => throw new ArgumentException(),
            <30 when reading.Pressure < 10m => Outlook.Cool,
            >50 => Outlook.Good,
            _ => Outlook.Warm
        };
        //{
        //    get
        //    {
        //        if (reading.Equals(new Reading()))
        //        {
        //            throw new ArgumentException();
        //        }
        //        else
        //        {
        //            if (reading.Pressure < 10m && reading.Temperature < 30m)
        //            {
        //                return Outlook.Cool;
        //            }
        //            else if (reading.Temperature > 50)
        //            {
        //                return Outlook.Good;
        //            }
        //            else
        //            {
        //                return Outlook.Warm;
        //            }
        //        }
        //    }
        //}

        public Outlook LongTermOutlook => reading.WindDirection switch
        {
            WindDirection.Southerly => Outlook.Good,
            WindDirection.Easterly when reading.Temperature > 20 => Outlook.Good,
            WindDirection.Northerly => Outlook.Cool,
            WindDirection.Easterly => Outlook.Warm,
            WindDirection.Westerly => Outlook.Rainy,
            _ => throw new ArgumentException()
        };
        //{
        //    get
        //    {
        //        if (reading.WindDirection == WindDirection.Southerly
        //            || reading.WindDirection == WindDirection.Easterly
        //            && reading.Temperature > 20)
        //        {
        //            return Outlook.Good;
        //        }
        //        if (reading.WindDirection == WindDirection.Northerly)
        //        {
        //            return Outlook.Cool;
        //        }
        //        if (reading.WindDirection == WindDirection.Easterly
        //            && reading.Temperature <= 20)
        //        {
        //            return Outlook.Warm;
        //        }
        //        if (reading.WindDirection == WindDirection.Westerly)
        //        {
        //            return Outlook.Rainy;
        //        }
        //        throw new ArgumentException();
        //    }
        //}

        public State RunSelfTest() => reading.Equals(new Reading())?State.Bad:State.Good;
        //{
        //    if (reading.Equals(new Reading()))
        //    {
        //        return State.Bad;
        //    }
        //    else
        //    {
        //        return State.Good;
        //    }
        //}
    }

    /*** Please do not modify this struct ***/
    public struct Reading
    {
        public decimal Temperature { get; }
        public decimal Pressure { get; }
        public decimal Rainfall { get; }
        public WindDirection WindDirection { get; }

        public Reading(decimal temperature, decimal pressure,
            decimal rainfall, WindDirection windDirection)
        {
            Temperature = temperature;
            Pressure = pressure;
            Rainfall = rainfall;
            WindDirection = windDirection;
        }
    }

    /*** Please do not modify this enum ***/
    public enum State
    {
        Good,
        Bad
    }

    /*** Please do not modify this enum ***/
    public enum Outlook
    {
        Cool,
        Rainy,
        Warm,
        Good
    }

    /*** Please do not modify this enum ***/
    public enum WindDirection
    {
        Unknown, // default
        Northerly,
        Easterly,
        Southerly,
        Westerly
    }
}

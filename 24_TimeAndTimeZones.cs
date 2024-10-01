using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code
{
    public enum Location
    {
        NewYork,
        London,
        Paris
    }

    public enum AlertLevel
    {
        Early,
        Standard,
        Late
    }

    public static class Appointment
    {
        public static DateTime ShowLocalTime(DateTime dtUtc) => dtUtc.ToLocalTime();

        public static DateTime Schedule(string appointmentDateDescription, Location location) 
        {
            DateTime dt = DateTime.ParseExact(appointmentDateDescription,"M/d/yyyy H:m:s",System.Globalization.CultureInfo.InvariantCulture);
            switch (location){
                case Location.NewYork:
                    return TimeZoneInfo.ConvertTimeToUtc(dt, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"));
                case Location.London:
                    return TimeZoneInfo.ConvertTimeToUtc(dt, TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time"));
                case Location.Paris:
                    return TimeZoneInfo.ConvertTimeToUtc(dt, TimeZoneInfo.FindSystemTimeZoneById("W. Europe Standard Time"));
                default:
                    throw new NotImplementedException();
            }
                
        }

        public static DateTime GetAlertTime(DateTime appointment, AlertLevel alertLevel) => alertLevel switch
        {
            AlertLevel.Early => appointment.AddDays(-1),
            AlertLevel.Standard => appointment.AddMinutes(-105),
            AlertLevel.Late => appointment.AddMinutes(-30)
        };

        public static bool HasDaylightSavingChanged(DateTime dt, Location location)
        {
            bool resultNow = false;
            bool resultSevenDaysEarlier = false;
            switch (location)
            {
                case Location.NewYork:
                    resultNow = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time").IsDaylightSavingTime(dt);
                    resultSevenDaysEarlier = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time").IsDaylightSavingTime(dt.AddDays(-7));
                    break;
                case Location.London:
                     resultNow = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time").IsDaylightSavingTime(dt);
                     resultSevenDaysEarlier = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time").IsDaylightSavingTime(dt.AddDays(-7));
                    break;
                case Location.Paris:
                     resultNow = TimeZoneInfo.FindSystemTimeZoneById("W. Europe Standard Time").IsDaylightSavingTime(dt);
                     resultSevenDaysEarlier = TimeZoneInfo.FindSystemTimeZoneById("W. Europe Standard Time").IsDaylightSavingTime(dt.AddDays(-7));
                    break;
            }
            
            if (resultNow != resultSevenDaysEarlier)
            {
                return true;
            }
            else
            {
                return false;
            }

            //DateTime utcTime = Schedule(dt.ToString("M/d/yyyy H:m:s"), location);
            //DateTime sevenDaysEarlier = utcTime.AddDays(-7);
            //if (DateTime.UtcNow > sevenDaysEarlier) { return true; } else { return false; }
        }

        public static DateTime NormalizeDateTime(string dtStr, Location location)
        {
            DateTime result = new DateTime();
            bool boolResult = false;
            switch (location)
            {
                case Location.London:
                    boolResult = DateTime.TryParse(dtStr, System.Globalization.CultureInfo.GetCultureInfo("en-GB"),System.Globalization.DateTimeStyles.None,out result);
                    break;
                case Location.NewYork:
                    boolResult = DateTime.TryParse(dtStr, System.Globalization.CultureInfo.GetCultureInfo("en-US"), System.Globalization.DateTimeStyles.None,out result);
                    break;
                case Location.Paris:
                    boolResult = DateTime.TryParse(dtStr, System.Globalization.CultureInfo.GetCultureInfo("fr-FR"), System.Globalization.DateTimeStyles.None,out result);
                    break;
                default:
                    boolResult = false;
                    break;
            }

            if (boolResult)
            {
                return result;
            }
            else
            {
                return new DateTime(year: 1, month: 1, day: 1);
            }
        }
    }
}

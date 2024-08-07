using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    static class Appointment
    {
        public static DateTime Schedule(string appointmentDateDescription)
        {
            //return DateTime.Parse(appointmentDateDescription, new CultureInfo("en-US");

            int lastIndexOf = appointmentDateDescription.LastIndexOf(" ");
            string time = appointmentDateDescription.Substring(lastIndexOf + 1);
            string date = appointmentDateDescription.Substring(0, lastIndexOf);

            string[] splittedTime = time.Split(':');

            string[] splittedDate;
            if (date.Contains('/'))
            {
                splittedDate = date.Split('/');
                return new DateTime(int.Parse(splittedDate[2]), int.Parse(splittedDate[0]), int.Parse(splittedDate[1]), int.Parse(splittedTime[0]), int.Parse(splittedTime[1]), int.Parse(splittedTime[2]));
            }
            else
            {
                splittedDate = date.Split(" ");
                for (int i = 0; i<splittedDate.Length; i++)
                {
                    splittedDate[i] = splittedDate[i].Replace(",", "");
                }
                //splittedDate = splittedDate.Select(x => x.Replace(",", string.Empty)).ToArray<>;

                if (splittedDate.Length > 3)
                {
                    splittedDate =new string[] {splittedDate[1], splittedDate[2], splittedDate[3]};
                }

                switch (splittedDate[0])
                {
                    case "January":
                        splittedDate[0] = "1";
                        break;
                    case "February":
                        splittedDate[0] = "2";
                        break;
                    case "March":
                        splittedDate[0] = "3";
                        break;
                    case "April":
                        splittedDate[0] = "4";
                        break;
                    case "May":
                        splittedDate[0] = "5";
                        break;
                    case "June":
                        splittedDate[0] = "6";
                        break;
                    case "July":
                        splittedDate[0] = "7";
                        break;
                    case "Agoust":
                        splittedDate[0] = "8";
                        break;
                    case "Septembre":
                        splittedDate[0] = "9";
                        break;
                    case "Octobre":
                        splittedDate[0] = "10";
                        break;
                    case "Novembre":
                        splittedDate[0] = "11";
                        break;
                    case "December":
                        splittedDate[0] = "12";
                        break;
                }

                return new DateTime(int.Parse(splittedDate[2]), int.Parse(splittedDate[0]), int.Parse(splittedDate[1]), int.Parse(splittedTime[0]), int.Parse(splittedTime[1]), int.Parse(splittedTime[2]));

            }
        }

        public static bool HasPassed(DateTime appointmentDate) => (DateTime.Compare( DateTime.Now ,appointmentDate)) <= 0 ?  false: true;

        public static bool IsAfternoonAppointment(DateTime appointmentDate) => (appointmentDate.Hour >=12 && appointmentDate.Hour < 18) ? true : false;

        public static string Description(DateTime appointmentDate) => $"You have an appointment on {appointmentDate.ToString("MM/dd/yy h:mm:ss tt")}.";


        public static DateTime AnniversaryDate() 
        {
            DateTime nowDatetime = DateTime.Now;
            return new DateTime(nowDatetime.Year, 9, 15, 0, 0, 0);

        }
    }



    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------


    public static class Gigasecond
    {
        public static DateTime Add(DateTime moment)
        {
            DateTime result = new DateTime();
            result = moment.AddSeconds(1e9);
            return result;
        }
    }



    //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    
    
    public enum Schedule
    {
        Teenth,
        First,
        Second,
        Third,
        Fourth,
        Last
    }

    public class Meetup
    {

        private int _month;
        private int _year;

        public Meetup(int month, int year)
        {
            _month = month;
            _year = year;
        }

        public DateTime Day(DayOfWeek dayOfWeek, Schedule schedule)
        {
            DateTime firstDayOfMonth = new DateTime(_year, _month, 1);
            int dayOfWeekFirstDayOfMonth = (int)firstDayOfMonth.DayOfWeek;      //0=Sunday, 1=Monday, 2=Tuesday, etc

            
            int offset =  (int)dayOfWeek - dayOfWeekFirstDayOfMonth;
            if (offset < 0)
            {
                offset +=7;
            }
            int firstDayOfMonthForThisDayOfWeek = 1 + offset;

            switch (schedule)
            {
                //case Schedule.First:
                //    return new DateTime(_year, _month, firstDayOfMonthForThisDayOfWeek);
                //    break;
                case Schedule.Second:
                    return new DateTime(_year, _month, firstDayOfMonthForThisDayOfWeek + 7);
                    break;
                case Schedule.Third:
                    return new DateTime(_year, _month, firstDayOfMonthForThisDayOfWeek + 14);
                    break;
                case Schedule.Fourth:
                    return new DateTime(_year, _month, firstDayOfMonthForThisDayOfWeek + 21);
                    break;
                case Schedule.Teenth:
                    if(firstDayOfMonthForThisDayOfWeek <6)
                    {
                        return new DateTime(_year, _month, firstDayOfMonthForThisDayOfWeek + 14);
                    }
                    else {
                        return new DateTime(_year, _month, firstDayOfMonthForThisDayOfWeek + 7);
                    }
                    break;
                case Schedule.Last:
                    DateTime lastDayOfMonth = new DateTime(_year, _month, (int)DateTime.DaysInMonth(_year, _month));
                    int dayOfWeekLastDayOfMonth = (int)lastDayOfMonth.DayOfWeek;
                    int offset2 = (int)dayOfWeek - dayOfWeekLastDayOfMonth;
                    if (offset2 > 0)
                    {
                        offset2 -= 7;
                    }
                    return new DateTime(_year, _month, (int)DateTime.DaysInMonth(_year, _month) + offset2);
                    break;
                default:
                    return new DateTime(_year, _month, firstDayOfMonthForThisDayOfWeek);
                    break;
            }
        }
    }
}

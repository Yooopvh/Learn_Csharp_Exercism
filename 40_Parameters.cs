using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code
{
    public class RemoteControlCar
    {
        private int batteryPercentage = 100;
        private int distanceDrivenInMeters = 0;
        private string[] sponsors = new string[0];
        private int latestSerialNum = 0;

        public void Drive()
        {
            if (batteryPercentage > 0)
            {
                batteryPercentage -= 10;
                distanceDrivenInMeters += 2;
            }
        }

        public void SetSponsors(params string[] sponsors) => this.sponsors = sponsors;

        public string DisplaySponsor(int sponsorNum) => sponsors[sponsorNum];


        public bool GetTelemetryData(ref int serialNum,
            out int batteryPercentage, out int distanceDrivenInMeters)
        {
            if (serialNum < latestSerialNum)
            {
                serialNum = latestSerialNum;
                batteryPercentage = distanceDrivenInMeters = -1;
                return false;
            }
            else
            {
                latestSerialNum = serialNum;
                batteryPercentage = this.batteryPercentage;
                distanceDrivenInMeters = this.distanceDrivenInMeters;
                return true;
            }
        }

        public static RemoteControlCar Buy() => new RemoteControlCar();

    }

    public class TelemetryClient
    {
        private RemoteControlCar car;

        public TelemetryClient(RemoteControlCar car)
        {
            this.car = car;
        }

        public string GetBatteryUsagePerMeter(int serialNum)
        {
            int batteryPercentage;
            int distanceDrivenInMeters;
            bool telemetryResult = this.car.GetTelemetryData(ref serialNum,out batteryPercentage ,out distanceDrivenInMeters );

            string result = (!telemetryResult || distanceDrivenInMeters == 0) ? "no data" : $"usage-per-meter={(100-batteryPercentage) /distanceDrivenInMeters}";
            return result ;
        }   
    }
}

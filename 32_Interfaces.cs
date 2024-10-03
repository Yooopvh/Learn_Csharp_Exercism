using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test;

namespace Code
{
    // TODO implement the IRemoteControlCar interface
    public interface IRemoteControlCar
    {
        void Drive();
        int DistanceTravelled { get; }
    }

    public class ProductionRemoteControlCar : IRemoteControlCar, IComparable<ProductionRemoteControlCar>
    {
        public int DistanceTravelled { get; private set; }
        public int NumberOfVictories { get; set; }

        public void Drive()
        {
            DistanceTravelled += 10;
        }

        public int CompareTo(ProductionRemoteControlCar otherCar)
        {
            if (NumberOfVictories == otherCar.NumberOfVictories) { return 0; }
            else if (NumberOfVictories < otherCar.NumberOfVictories) { return -1; }
            else { return 1; }
        }
    }

    public class ExperimentalRemoteControlCar : IRemoteControlCar
    {
        public int DistanceTravelled { get; private set; }

        public void Drive()
        {
            DistanceTravelled += 20;
        }
    }

    public static class TestTrack
    {
        public static void Race(IRemoteControlCar car)
        {
            car.Drive();
        }

        public static List<ProductionRemoteControlCar> GetRankedCars(ProductionRemoteControlCar prc1, ProductionRemoteControlCar prc2)
        {
            List<ProductionRemoteControlCar> resultList = new List<ProductionRemoteControlCar>();
            if (prc1.CompareTo(prc2) < 0) { resultList.Add(prc1); resultList.Add(prc2); }
            else { resultList.Add(prc2); resultList.Add(prc1); }
            return resultList;
        }
    }
}

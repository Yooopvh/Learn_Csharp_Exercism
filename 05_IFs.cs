using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    static class AssemblyLine
    {
        public static double SuccessRate(int speed)
        {
            if (speed == 0) return 0;
            else if (speed <= 4) return 1;
            else if (speed <= 8) return 0.9;
            else if (speed == 9) return 0.8;
            else return 0.77;
        }

        public static double ProductionRatePerHour(int speed) => speed*221 * SuccessRate(speed);

        public static int WorkingItemsPerMinute(int speed) => (int) Math.Floor(ProductionRatePerHour(speed)/60);
    }

    //public static int Score(double x, double y)
    //{
    //    double dartRadious = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
    //    if (dartRadious <= 1) return 10;
    //    else if (dartRadious <= 5) return 5;
    //    else if (dartRadious <= 10) return 1;
    //    else return 0;
    //}


    public static class Triangle
    {
        public static bool IsScalene(double side1, double side2, double side3)
        {
            return (side1 != side2 && side2 != side3 && side1 != side3) && IsTriangle(side1,side2,side3);
        }

        public static bool IsIsosceles(double side1, double side2, double side3)
        {
            return (side1 == side2 ||  side2 == side3 || side3 == side1) && IsTriangle(side1,side2,side3);
        }

        public static bool IsEquilateral(double side1, double side2, double side3)
        {
            return (side1 == side2 && side2 == side3) && IsTriangle(side1, side2, side3);
        }

        public static bool IsTriangle(double side1, double side2, double side3)
        {
            if ((side1 > 0 && side2 > 0 && side3 > 0) && (side3 + side2 >= side1 && side1 + side2 >= side3 && side1 + side3 >= side2)) return true; return false;
        }
    }

}

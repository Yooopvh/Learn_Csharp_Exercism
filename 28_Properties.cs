using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code
{
    class WeighingMachine
    {
        public int Precision { get; }
        private double _weight;
        public double TareAdjustment { get; set; } = 5;

        public double Weight
        {
            get { return _weight; }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Weight can´t be negative.");
                }
                else
                {
                    _weight = value;
                }
            }
        }

        public WeighingMachine(int precision)
        {
            Precision = precision;
        }
        

        public string DisplayWeight
        {
            get { return $"{Math.Round(_weight -  TareAdjustment,Precision).ToString($"F{Precision}")} kg";  }
        }
    }
}

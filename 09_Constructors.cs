using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class RemoteControlCar
    {
        public int batteryPercentage;
        private int _drivenMeters;
        public int speed;
        public int batteryConsumption;
        
        public RemoteControlCar(int speeed, int battery)
        {
            this.batteryPercentage = 100;
            this.speed = speeed;
            this._drivenMeters = 0;
            this.batteryConsumption = battery;
        }

        public bool BatteryDrained() => batteryPercentage >= batteryConsumption ? false : true;

        public int DistanceDriven() =>  _drivenMeters;


        public void Drive()
        {
            if (batteryPercentage >= batteryConsumption)
            {
                _drivenMeters += speed;
                batteryPercentage -= batteryConsumption;
            }
        }

        public static RemoteControlCar Nitro() => new RemoteControlCar(50,4);

    }

    class RaceTrack
    {
        private int _trackLength; 

        public RaceTrack(int trackLength)
        {
            this._trackLength = trackLength;
        }

        public bool TryFinishTrack(RemoteControlCar car) => (((double)this._trackLength/(double)car.speed)*(double)car.batteryConsumption > 100) ? false : true;

    }


    //--------------------------------------------------------------------------------------------------------------------------------------------------


    public class RailFenceCipher
    {
        private int _numOfRails;
        private string _solutionEncoded;

        public RailFenceCipher(int rails) => _numOfRails = rails;

        public string Encode(string input)
        {
            int generalStep = (_numOfRails - 1) *2;

            for (int j = 1; j <= _numOfRails; j++)
            {
                for (int i= (j-1); i < input.Length ; i += generalStep)
                {
                    int step1 = generalStep - (j-1)*2;
                    int step2 = generalStep - step1;

                    _solutionEncoded += input[i];
                    if (step1 != 0 && step2 != 0 && i+step1 <= input.Length-1)
                    {
                        _solutionEncoded += input[i+step1];
                    }
                }
            }
            return _solutionEncoded;
        }

        public string Decode(string input)
        {
            string solutionDecoded = new string('-',input.Length);
            char[] solutionDecodedArray = solutionDecoded.ToCharArray();
            int index = 0;

            int generalStep = (_numOfRails - 1) *2;

            for (int j = 1; j <= _numOfRails; j++)
            {
                for (int i = (j-1); i < input.Length ; i += generalStep)
                {
                    int step1 = generalStep - (j-1)*2;
                    int step2 = generalStep - step1;

                    solutionDecodedArray[i] = input[index];
                    index++;
                    if (step1 != 0 && step2 != 0 && i+step1 <= input.Length-1)
                    {
                        solutionDecodedArray[i+step1] = input[index];
                        index++;
                    }
                }
            }

            return new string(solutionDecodedArray);
        }
    }

}

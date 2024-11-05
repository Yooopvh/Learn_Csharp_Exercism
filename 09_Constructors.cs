using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
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


    //----------------------------------------------------------------------------------------------------------------------------------------------------

    public class BinarySearchTree : IEnumerable<int>
    {
        private BinarySearchTree? _left = null;
        private BinarySearchTree? _right = null;
        private int? _value = null;
        public BinarySearchTree(int value)
        {
            _value = value;
        }

        public BinarySearchTree(IEnumerable<int> values)
        {
            _value = values.First();
            for (int i = 1; i < values.Count();i++) this.Add(values.ElementAt(i));
        }

        public int Value
        {
            get
            {
                return _value is null ? throw new Exception() : (int)_value;
            }
        }

        public BinarySearchTree Left
        {
            get
            {
                return _left;
            }
        }

        public BinarySearchTree Right
        {
            get
            {
                return _right;
            }
        }

        public BinarySearchTree Add(int value)
        {
            if (value > _value)
            {
                _right = _right is null ? new BinarySearchTree(value) : _right.Add(value);
            } else
            {
                _left = _left is null ? new BinarySearchTree(value) : _left.Add(value);
            }
            return this;
        }

        public IEnumerator<int> GetEnumerator()
        {
            if (Left != null)
                foreach (var leftValue in Left)
                    yield return leftValue;
            yield return Value;
            if (Right != null)
                foreach (var rightValue in Right)
                    yield return rightValue;
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }

    //----------------------------------------------------------------------------------------------------------------------------------------------------

    public enum Bucket
    {
        One,
        Two
    }

    public class TwoBucketResult
    {
        public int Moves { get; set; }
        public Bucket GoalBucket { get; set; }
        public int OtherBucket { get; set; }
    }

    public class TwoBucket
    {
        private readonly int _bucketOneCapacity;
        private readonly int _bucketTwoCapacity;
        private readonly Bucket _startBucket;
        private int _litersGoal;
        private int _litersInBucket1 = 0;
        private int _litersInBucket2 = 0;
        private List<(int,int)> _previousResults = new List<(int,int)>();
        private List<TwoBucketResult> resultsList = new List<TwoBucketResult>();
        public TwoBucket(int bucketOne, int bucketTwo, Bucket startBucket)
        {
            _bucketOneCapacity = bucketOne;
            _bucketTwoCapacity = bucketTwo;
            _startBucket = startBucket;

            if (startBucket == Bucket.One) _litersInBucket1 = bucketOne;
            else _litersInBucket2 = bucketTwo;
        }

        public TwoBucketResult Measure(int goal)
        {
            _litersGoal = goal;
            _previousResults.Add((_litersInBucket1, _litersInBucket2));

            while (_litersInBucket1 != _litersGoal && _litersInBucket2 != _litersGoal)
            {
                (int, int)? result = null;
                result = Calculate();

                if (resultsList.Count == 0) throw new ArgumentException(); 
                else 
                {
                    TwoBucketResult bestResult = resultsList.First();
                    foreach (TwoBucketResult result2 in resultsList)
                    {
                        if (result2.Moves < bestResult.Moves)
                        {
                            bestResult = result2;
                        }
                    }
                    return bestResult;
                };
            }
            return new TwoBucketResult() { Moves = 1, GoalBucket = _startBucket, OtherBucket = 0 };

        }

        private (int,int)? Calculate()
        {

            while (_litersInBucket1 != _litersGoal && _litersInBucket2 != _litersGoal)
            {
                List<(int, int)> posibleSolutions = new List<(int, int)>();

                //Prueba todas las combinaciones a ver cuantas son factibles
                if (IsValid(PassFromOneToTwo())) posibleSolutions.Add(PassFromOneToTwo());
                if (IsValid(PassFromTwoToOne())) posibleSolutions.Add(PassFromTwoToOne());
                if (IsValid(FillOne())) posibleSolutions.Add(FillOne());
                if (IsValid(FillTwo())) posibleSolutions.Add(FillTwo());
                if (IsValid(EmptyOne())) posibleSolutions.Add(EmptyOne());
                if (IsValid(EmptyTwo())) posibleSolutions.Add(EmptyTwo());

                if (posibleSolutions.Count > 0)
                {
                    foreach ((int, int) solution in posibleSolutions)
                    {
                        if (solution.Item1 == _litersGoal || solution.Item2 == _litersGoal)
                        {
                            Bucket goalBucket = solution.Item1 == _litersGoal ? Bucket.One : Bucket.Two;
                            int otherBucket = solution.Item1 == _litersGoal ? solution.Item2 : solution.Item1;
                            resultsList.Add(new TwoBucketResult() { Moves = _previousResults.Count()+1, GoalBucket = goalBucket, OtherBucket = otherBucket });
                        } //return solution;
                        else
                        {
                            List<(int, int)> previousResultsSafety = new List<(int, int)>(_previousResults);
                            _previousResults.Add(solution);
                            (int, int)? result = null;
                            _litersInBucket1 = solution.Item1;
                            _litersInBucket2 = solution.Item2;
                            result = Calculate();

                            if (result is null) _previousResults = previousResultsSafety;
                            else return result;
                        }

                    }
                    return null; 
                }
                else return null;

            }
            return (_litersInBucket1, _litersInBucket2);
        }

        public bool IsValid((int,int) result) =>  (result.Item1 == _litersGoal || result.Item2 == _litersGoal) ||
            (result != (0,0) && 
            result != (0,_bucketTwoCapacity) &&
            result != (_bucketOneCapacity,0) &&
            result != (_bucketOneCapacity,_bucketTwoCapacity) &&
            !_previousResults.Contains(result) &&
            result.Item1 >= 0 &&
            result.Item2 >= 0 &&
            result.Item1 <= _bucketOneCapacity &&
            result.Item2 <= _bucketTwoCapacity);

        private (int,int) PassFromOneToTwo()
        {
            int availableCapacity = _bucketTwoCapacity - _litersInBucket2;
            int bucket1Result = Math.Max(_litersInBucket1 - availableCapacity, 0);
            int bucket2Result = Math.Min(_bucketTwoCapacity, _litersInBucket2 + _litersInBucket1);
            return (bucket1Result, bucket2Result);
        }

        private (int,int) PassFromTwoToOne()
        {
            int availableCapacity = _bucketOneCapacity - _litersInBucket1;
            int bucket2Result = Math.Max(_litersInBucket2 - availableCapacity, 0);
            int bucket1Result = Math.Min(_bucketOneCapacity, _litersInBucket2 + _litersInBucket1);
            return (bucket1Result, bucket2Result);
        }

        private (int,int) EmptyOne()
        {
            int bucket1Result = 0;
            int bucket2Result = _litersInBucket2;
            return(bucket1Result, bucket2Result);
        }

        private (int,int) EmptyTwo()
        {
            int bucket1Result = _litersInBucket1;
            int bucket2Result = 0;
            return (bucket1Result, bucket2Result);
        }

        private (int,int) FillOne()
        {
            int bucket1Result = _bucketOneCapacity;
            int bucket2Result = _litersInBucket2;
            return (bucket1Result, bucket2Result);
        }

        private (int,int) FillTwo()
        {
            int bucket1Result = _litersInBucket1;
            int bucket2Result = _bucketTwoCapacity;
            return (bucket1Result, bucket2Result);
        }
    }


    //----------------------------------------------------------------------------------------------------------------------------------------------------

    public class SgfTree
    {
        public SgfTree(IDictionary<string, string[]> data, params SgfTree[] children)
        {
            Data = data;
            Children = children;
        }

        public IDictionary<string, string[]> Data { get; }
        public SgfTree[] Children { get; }
    }

    //public class SgfParser
    //{
    //    public static SgfTree ParseTree(string input)
    //    {
    //        string[] inputByLevels = input.Split(';');

    //        if (inputByLevels.Length <2)
    //        {
    //            throw new ArgumentException();
    //        }

    //        string levelInput = inputByLevels[1];
    //        Dictionary<string, string[]> data = new Dictionary<string, string[]>();
    //        string pattern = @"([A-Z]{1,2}(?:\[([a-zA-Z;0-9]+)\])+)";
    //        //Iteramos sobre las keys de data
    //        foreach (Match match in Regex.Matches(levelInput, pattern))
    //        {
    //            int numGroups = match.Groups.Count;
    //            string[] values = match.Groups[2].Captures.Select(x => x.ToString()).ToArray();
    //            data.Add(match.Value.Substring(0, match.Value.IndexOf('[')), values);
    //        }

    //        if (data.Count == 0)
    //        {
    //            throw new ArgumentException();
    //        }

    //        if (levelInput.EndsWith(')'))
    //        {
    //            SgfTree result = new SgfTree(data);
    //            return result;
    //        } else
    //        {
    //            int i = inputByLevels.ToList().IndexOf(levelInput);
    //            SgfTree subTree = ParseTree(input.Substring(input.IndexOf(';',input.IndexOf(';')+1)));
    //            SgfTree result = new SgfTree(data, subTree);
    //            return result;
    //        }
            
    //    }
    //}

    public class SgfParser
    {
        public static SgfTree ParseTree(string input)
        {
            if (input == "(;)") return new SgfTree(new Dictionary<string, string[]>());

            //string pattern = @"(\(*;?([A-Z]{1,2}(?:\[([a-zA-Z;0-9]+)\])+)\)*)";
            string pattern = @"(\(*;?([A-Z]{1,2}(?:\[([^\[\]]+)\])+)\)*)";
            MatchCollection matches = Regex.Matches(input, pattern);

            if (matches.Count == 0) throw new ArgumentException();

            Dictionary<string, string[]> data = new Dictionary<string, string[]>();

            int numGroups = matches[0].Groups.Count;
            string groupString = matches[0].Groups[numGroups-2].Value;
            string key = groupString.Substring(0,groupString.IndexOf('['));
            string[] values = matches[0].Groups[numGroups-1].Captures.Select(x => x.Value).ToArray();
            data.Add(key, values);

            // Comprobar si alguno de los siguientes grupos no contiene ;. Si no lo contiene, estará en el mismo nivel
            int lastIndexGroup = 1;
            for (int i = 1; i < matches.Count; i++)
            {
                groupString = matches[i].Groups[0].Value;
                if (groupString.Contains(';'))
                {
                    lastIndexGroup = i;
                    break;
                } else
                {
                    key = groupString.Substring(0, groupString.IndexOf('['));
                    values = matches[i].Groups[numGroups-1].Captures.Select(x => x.Value).ToArray();
                    data.Add(key,values);
                    lastIndexGroup = i+1;
                }
            }

            List<string> subStringsForSubDictionaries = new List<string>();
            string subString = string.Empty;
            int openedParenthesis = 0;

            for (int i = lastIndexGroup; i < matches.Count;i++)
            {
                openedParenthesis += matches[i].Value.Count(x => x == '(');

                subString += matches[i].Value;
                int closingParenthesisCount = matches[i].Value.Count(x => x == ')');
                if (closingParenthesisCount >= openedParenthesis)
                {
                    subStringsForSubDictionaries.Add(subString);
                    subString = string.Empty;
                }

                openedParenthesis -= closingParenthesisCount;
            }

            if (subStringsForSubDictionaries.Count == 0)
            {
                return new SgfTree(data);
            } else
            {
                return new SgfTree(data, subStringsForSubDictionaries.Select(x => ParseTree(x)).ToArray());
            }

        }
    }


    //-----------------------------------------------------------------------------------------------------------------------------------------------

    public class CustomSet : IEquatable<CustomSet> 
    {
        int[] _values;
        public CustomSet(params int[] values)
        {
            _values = values;
        }

        public CustomSet Add(int value)
        {
            CustomSet result = new CustomSet(_values);
            if (!_values.Contains(value))
            {
                Array.Resize(ref result._values, _values.Length+1);
                result._values[_values.Length] = value;
            }
            return result;
        }

        public bool Empty() => _values.Count() == 0;


        public bool Contains(int value) => _values.Contains(value);

        public bool Subset(CustomSet right)
        {
            bool result = true;
            foreach (int value in _values)
            {
                if (!right._values.Contains(value)) result = false;
            };
            return result;
        }

        public bool Disjoint(CustomSet right)
        {
            bool result = true;
            foreach (int value in right._values)
            {
                if (_values.Contains(value)) result = false;
            };
            return result;
        }

        public CustomSet Intersection(CustomSet right)
        {
            CustomSet result = new CustomSet();
            foreach (int value in right._values)
            {
                if (_values.Contains(value))
                {
                    result = result.Add(value);
                }
            }
            return result;  
        }

        public CustomSet Difference(CustomSet right)
        {
            CustomSet result = new CustomSet();
            foreach (int value in _values)
            {
                if (!right._values.Contains(value))
                {
                    result = result.Add(value);
                }
            }
            return result;
        }

        public CustomSet Union(CustomSet right)
        {
            CustomSet result = new CustomSet(_values);
            foreach (int value in right._values)
            {
                if (!_values.Contains(value))
                {
                    result = result.Add(value);
                }
            }
            return result;
        }

        public bool Equals(CustomSet? other) => _values.OrderBy(x => x).ToArray().SequenceEqual(other._values.OrderBy(x => x).ToArray());

    }
}

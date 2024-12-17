using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Code
{
    public class Manager
    {
        public string Name { get; }

        public string? Club { get; }

        public Manager(string name, string? club)
        {
            this.Name = name;
            this.Club = club;
        }
    }

    public class Incident
    {
        public virtual string GetDescription() => "An incident happened.";
    }

    public class Foul : Incident
    {
        public override string GetDescription() => "The referee deemed a foul.";
    }

    public class Injury : Incident
    {
        private readonly int player;

        public Injury(int player)
        {
            this.player = player;
        }

        public override string GetDescription() => $"Player {player} is injured.";
    }
    public static class PlayAnalyzer
    {
        public static string AnalyzeOnField(int shirtNum) 
        {
            //switch(shirtNum)
            //{
            //    case 1:
            //        return "goalie";
            //    case 2:
            //        return "left back";
            //    case 3:
            //    case 4:
            //        return "center back";
            //    case 5:
            //        return "right back";
            //    case 6:
            //    case 7:
            //    case 8:
            //        return "midfielder";
            //    case 9:
            //        return "left wing";
            //    case 10:
            //        return "striker";
            //    case 11:
            //        return "right wing";
            //    default:
            //        throw new ArgumentOutOfRangeException(nameof(shirtNum), $"Not expected shirt number: {shirtNum}");

            return shirtNum switch
            {
                1 => "goalie",
                2 => "left back",
                3 or 4 => "center back",
                5 => "right back",
                6 or 7 or 8 => "midfielder",
                9 => "left wing",
                10 => "striker",
                11 => "right wing",
                _ => throw new ArgumentOutOfRangeException(nameof(shirtNum), $"Not expected shirt number: {shirtNum}")
            };

        }

        public static string AnalyzeOffField(object report)
        {
            switch (report)
            {
                case string announcements:
                    return announcements;
                case int supporters:
                    return $"There are {supporters} supporters at the match.";
                case Foul foul:
                    return foul.GetDescription();
                case Injury injury:
                    return $"Oh no! {injury.GetDescription()} Medics are on the field.";
                case Incident incident:
                    return incident.GetDescription();
                case Manager manager:
                    string message = $"{manager.Name}";
                    if (manager.Club != null) message += $" ({manager.Club})";
                    return message ;
                default:
                    throw new ArgumentException();
            }
        }
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------------

    public static class Wordy
    {
        public static int Answer(string question)
        {
            int[] numbers = Regex.Matches(question,@"-?\d+").Select(v => Int32.Parse(v.Value)).ToArray();
            //string[] operations = Regex.Matches(question, @"\d (plus|minus|divided by|multiplied by)+").Select(v => v.Value).ToArray();
            string[] operations = Regex.Matches(question, @"(?<=\d)( [a-z]* ?[a-z]*)")
                                        .Cast<Match>()
                                        .Select(v => v.Groups[1].Value)
                                        .ToArray();

            if (numbers.Length == 0 || numbers.Length -1 != operations.Length) throw new ArgumentException();

            for (int i = 0; i < numbers.Length - 1; i++)
            {
                switch (operations[i].Trim())
                {
                    case "plus":
                        numbers[i+1] = numbers[i] + numbers[i+1]; 
                        break;
                    case "minus":
                        numbers[i+1] = numbers[i] - numbers[i+1];
                        break;
                    case "divided by":
                        numbers[i+1] = numbers[i] / numbers[i+1];
                        break;
                    case "multiplied by":
                        numbers[i+1] = numbers[i] * numbers[i+1];
                        break;
                    default: throw new ArgumentException();
                }
            }

            return numbers.Last();
        }
    }

    //------------------------------------------------------------------------------------------------------------------------------------------------------

    public enum ConnectWinner
    {
        White,
        Black,
        None
    }

    public class Connect
    {
        private string[] _cleanInput;
        private ConnectWinner _result = ConnectWinner.None;
        private int _numRows;
        private int _numCols;
        public Connect(string[] input)
        {
            _cleanInput = input.Select(s => s.Replace(" ","")).ToArray();

            _numRows = _cleanInput.Length;
            _numCols = _cleanInput[0].Length;

            // Check O
            char player = 'O';
            ConnectWinner result;
            for (int j = 0;j < _numCols;j++)
            {
                result = checkNext(0, j, player, new List<(int, int)>());
                if (result != ConnectWinner.None) 
                {
                    _result = result; 
                    break;
                }
            }

            // Check X
            player = 'X';
            if (_result == ConnectWinner.None)
            {
                for (int i = 0; i < _numRows; i++)
                {
                    result = checkNext(i, 0, player, new List<(int, int)>());
                    if (result != ConnectWinner.None) _result = result;
                }
            }

        }

        public ConnectWinner Result() => _result;

        private ConnectWinner checkNext(int actualRow, int actualColumn, char player, List<(int,int)> alreadyChecked)
        {
            if (!alreadyChecked.Contains((actualRow, actualColumn)) && _cleanInput[actualRow][actualColumn] == player)
            {
                List<(int,int)> newList = new List<(int,int)> (alreadyChecked);
                newList.Add((actualRow, actualColumn));
                if (actualRow + 1 == _numRows && _cleanInput[actualRow][actualColumn] == 'O' && player == 'O') return ConnectWinner.White;
                if (actualColumn + 1 == _numCols && _cleanInput[actualRow][actualColumn] == 'X' && player == 'X') return ConnectWinner.Black;
                ConnectWinner result;
                if (actualRow > 0)
                {
                    if (_cleanInput[actualRow-1][actualColumn] == player)
                    {
                        result = checkNext(actualRow-1, actualColumn, player,newList);
                        if (result != ConnectWinner.None) return result;
                    };
                    if (actualColumn < _numCols - 1 && _cleanInput[actualRow-1][actualColumn+1] == player)
                    {
                        result = checkNext(actualRow-1, actualColumn+1, player, newList);
                        if (result != ConnectWinner.None) return result;
                    };
                }
                if (actualRow < _numRows-1)
                {
                    if (actualColumn > 0 && _cleanInput[actualRow+1][actualColumn - 1] == player)
                    {
                        result = checkNext(actualRow+1, actualColumn-1, player, newList);
                        if (result != ConnectWinner.None) return result;
                    };

                    if (_cleanInput[actualRow+1][actualColumn] == player)
                    {
                        result = checkNext(actualRow+1, actualColumn, player, newList);
                        if (result != ConnectWinner.None) return result;
                    };
                }
                if (actualColumn > 0 && _cleanInput[actualRow][actualColumn - 1] == player)
                {
                    result = checkNext(actualRow, actualColumn -1, player, newList);
                    if (result != ConnectWinner.None) return result;
                };
                if (actualColumn < _numCols - 1 && _cleanInput[actualRow][actualColumn + 1] == player)
                {
                    result = checkNext(actualRow, actualColumn + 1, player, newList);
                    if (result != ConnectWinner.None) return result;
                };
            }
            

            return ConnectWinner.None;
        }

    }
}

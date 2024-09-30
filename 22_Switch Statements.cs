using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
}

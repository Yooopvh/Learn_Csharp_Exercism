using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    static class Badge
    {
        public static string Print(int? id, string name, string? department) => (id == null ? $"{name} - {(department ??  "OWNER").ToUpper()}" : $"[{id}] - {name} - {(department ??  "OWNER").ToUpper()}");

        //BETTER SOLUTION
        //public static string Print(int? id, string name, string? department) => $"{(id == null ? "" : $"[{id}] - ")}{name} - {department?.ToUpper() ?? "OWNER"}";

    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------

    public class BowlingGame
    {
        private int[,] multiplier = { { 1, 1 }, { 1, 1 }, { 1, 1 }, { 1, 1 }, { 1, 1 }, { 1, 1 }, { 1, 1 }, { 1, 1 }, { 1, 1 }, { 1, 1 }, { 0, 0 }, { 0, 0 } };
        private int round = 1;
        private int subRound = 1;
        private int currentPins = 10;
        private int result = 0;
        public void Roll(int pins)
        {
            if (pins > 0) 
            { Debugger.Break(); }
            result += pins * multiplier[round-1, subRound - 1];
            if (currentPins - pins < 0 || pins < 0 || (round > 10 && multiplier[round-1,subRound-1]<1)) throw new ArgumentException();
            else if (currentPins - pins == 0 && subRound == 1 && round < 12)
            {
                int numToSum = round > 10 ? 0 : 1;
                multiplier[round,0] += multiplier[round-1,1] >= 1 ? multiplier[round-1,1]:numToSum;
                multiplier[round,1] += numToSum;
                NewRound();
            }
            else if (currentPins - pins == 0 && subRound == 2 && round < 11)
            {
                multiplier[round,0] += 1;
                NewRound();
            }
            else
            {
                if (subRound == 2 || currentPins - pins == 0) NewRound();
                else
                {
                    currentPins =currentPins - pins;
                    subRound++;
                }
            }
            
        }

        public void NewRound()
        {
            round++;
            subRound = 1;
            currentPins = 10;
        }

        public int? Score() => (round <= 10 || 
            (round == 11 && multiplier[10,subRound-1] > 0) || 
            (round == 12 && multiplier[11, subRound-1] > 0)) ? 
            throw new ArgumentException():result;

        public static void DoRoll(int[] previousRolls,ref BowlingGame game)
        {
            foreach (int roll in previousRolls)
            {
                game.Roll(roll);
            }
        }
    }
}

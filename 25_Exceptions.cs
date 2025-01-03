using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code
{
    public static class SimpleCalculator
    {
        public static string Calculate(int operand1, int operand2, string operation)
        {
            long result = 0;
            try
            {
                switch (operation)
                {
                    case "+":
                        result = operand1 + operand2;
                        break;
                    case "*":
                        result = operand1 * operand2;
                        break;
                    case "/":
                        if (operand2 == 0)
                        {
                            return "Division by zero is not allowed.";
                        }
                        result = operand1 / operand2;
                        break;
                    case "":
                        throw new ArgumentException();
                    case null:
                        throw new ArgumentNullException();
                    default:
                        throw new ArgumentOutOfRangeException();

                }
            } catch (Exception ex)
            {
                return ex.Message;
            }
            
            return $"{operand1} {operation} {operand2} = {result}";
        }

        //SOLUCIÓN COMUNIDAD
        //public static class SimpleCalculator
        //{
        //    public static string Calculate(int o1, int o2, string operation) =>
        //        operation switch
        //        {
        //            "*" => $"{o1} * {o2} = {o1 * o2}",
        //            "+" => $"{o1} + {o2} = {o1 + o2}",
        //            "/" => o2 != 0 ? $"{o1} / {o2} = {o1 / o2}" : "Division by zero is not allowed.",
        //            "" => throw new ArgumentException(),
        //            null => throw new ArgumentNullException(),
        //            _ => throw new ArgumentOutOfRangeException(),
        //        };
        //}

        //-------------------------------------------------------------------------------------------------------------------------------------------------------

        public static class Change
        {

            public static int[] FindFewestCoins(int[] coins, int target)
            {
                int pendingChange = target;
                if (target < 0) throw new ArgumentException();

                int[] dp = new int[target + 1]; //Almacena el número de monedas necesarias para alcanzar esa cantidad
                dp = dp.Select(x => int.MaxValue ).ToArray();
                dp[0] = 0;
                List<int>[] dp_coinsCombination = Enumerable.Range(0, dp.Length)
                                              .Select(x => new List<int>())
                                              .ToArray(); //Tabla para almacenar la combinación de monedas

                for (int i = 1; i < dp.Length; i++) {
                    foreach (int coin in coins)
                    {
                        if (i >= coin && dp[i - coin] < dp[i])
                        {
                            dp[i] = dp[i - coin] + 1;
                            dp_coinsCombination[i] = dp_coinsCombination[i-coin].Count > 0 ?  new List<int> (dp_coinsCombination[i - coin]) : new List<int>();
                            dp_coinsCombination[i].Add(coin);
                        }
                    }
                };

                if (target - dp_coinsCombination.Last().Sum() > 0) throw new ArgumentException();

                return dp_coinsCombination.Last().OrderBy(x=>x).ToArray();

            }
        }
    }
}

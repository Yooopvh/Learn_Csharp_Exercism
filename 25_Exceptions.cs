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
    }
}

using System;
using System.Collections.Generic;
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
}

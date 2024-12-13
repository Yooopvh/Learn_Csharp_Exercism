using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code
{
    public static class Languages
    {
        public static List<string> NewList() => new List<string>();

        public static List<string> GetExistingLanguages() => new List<string>() { "C#", "Clojure", "Elm" };

        public static List<string> AddLanguage(List<string> languages, string language) => languages.Append(language).ToList();

        public static int CountLanguages(List<string> languages) => languages.Count;

        public static bool HasLanguage(List<string> languages, string language) => languages.Contains(language);

        public static List<string> ReverseList(List<string> languages) => languages.Reverse<string>().ToList();

        public static bool IsExciting(List<string> languages)
        {
            bool result = false;
            if ((languages.Count >= 1 && languages[0]=="C#") || (languages.Count > 1 && languages[1] == "C#" &&  languages.Count < 4)) result = true;
            return result;
        }

        public static List<string> RemoveLanguage(List<string> languages, string language) => languages.Where(x => x != language).ToList();


        public static bool IsUnique(List<string> languages) => languages.Distinct().Count() == languages.Count();

    }

    //----------------------------------------------------------------------------------------------------------------------------------------------------------

    public static class ListOps
    {
        public static int Length<T>(List<T> input) => input.Count;

        public static List<T> Reverse<T>(List<T> input) => input.AsEnumerable().Reverse().ToList();

        public static List<TOut> Map<TIn, TOut>(List<TIn> input, Func<TIn, TOut> map) => input.Select(map).ToList();

        public static List<T> Filter<T>(List<T> input, Func<T, bool> predicate) => input.Where(predicate).ToList();

        public static TOut Foldl<TIn, TOut>(List<TIn> input, TOut start, Func<TOut, TIn, TOut> func)
        {
            foreach (var item in input)
            {
                start = func.Invoke(start, item);
            }
            return start;
        }

        public static TOut Foldr<TIn, TOut>(List<TIn> input, TOut start, Func<TIn, TOut, TOut> func)
        {
            foreach (var item in input.AsEnumerable().Reverse().ToList())
            {
                start = func.Invoke(item,start);
            }
            return start;
        }

        public static List<T> Concat<T>(List<List<T>> input)
        {
            List<T> result = new List<T>();
            foreach (List<T> subList in input)
            {
                foreach(T value in subList)
                {
                    result.Add(value);
                }
            }
            return result;
        }

        public static List<T> Append<T>(List<T> left, List<T> right)
        {
            List<T> result = left;
            foreach(T value in right)
            {
                result.Add(value);
            }
            return result;
        }
    }

    //------------------------------------------------------------------------------------------------------------------------------------------------------------

    public enum SublistType
    {
        Equal,
        Unequal,
        Superlist,
        Sublist
    }

    public static class Sublist
    {
        public static SublistType Classify<T>(List<T> list1, List<T> list2)
            where T : IComparable
        {
            List<T> iterableList = list1.Count > list2.Count ? list1 : list2;
            List<T> otherList = list1.Count > list2.Count ? list2 : list1;

            bool areEqualLength = list1.Count == list2.Count;
            bool l1BiggerThanL2 = list1.Count > list2.Count;

            if (list1.SequenceEqual(list2)) return SublistType.Equal;

            for (int i = 0; i < iterableList.Count; i++)
            {
                if (otherList.Count == 0 || iterableList[i].Equals(otherList.First()) )
                {
                    int subListLength = Math.Min(iterableList.Count - i, otherList.Count);
                    List<T> subList1 = iterableList.GetRange(i, subListLength);

                    // Hay coincidencia
                    if (subList1.SequenceEqual(otherList))
                    {
                        if(areEqualLength) return SublistType.Equal;
                        if(l1BiggerThanL2) return SublistType.Superlist;
                        return SublistType.Sublist;
                    }
                }
            }

            return SublistType.Unequal;
        }
    }
}

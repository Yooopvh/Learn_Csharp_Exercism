using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

namespace Test
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


        public static bool IsUnique(List<string> languages) =>  languages.Distinct().Count() == languages.Count();

    }


    //public class Deque<T>
    //{

    //    private List<int[]> list = new List<int[]>();
    //    int index = 0;
    //    int firstIndex = 0;
    //    int lastIndex = 0;
    //    int readIndex = -1;

    //    public void Push(int value)
    //    {
    //        if (index == 0)
    //        {
    //            list.Add( new int[] { -1 , value, -1 });
    //            index++;
    //        } else
    //        {
    //            list.Add(new int[] { -1, value, firstIndex });
    //            list[firstIndex][0] = index;
    //            firstIndex = index;
    //            index++;
    //        }
    //        list[firstIndex][0] = lastIndex;
    //        list[lastIndex][2] = firstIndex;
    //    }

    //    public int Pop()
    //    {
    //        if(readIndex == -1)
    //        {
    //            readIndex = firstIndex;
    //        } else
    //        {
    //            readIndex = list[readIndex][0];
    //        }
    //        return list[readIndex][1];
    //    }

    //    public void Unshift(int value)
    //    {
    //        if (index == 0)
    //        {
    //            list.Add(new int[] { -1, value, -1 });
    //            index++;
    //        }
    //        else
    //        {
    //            list.Add(new int[] { lastIndex, value, -1 });
    //            list[lastIndex][2] = index;
    //            lastIndex = index;
    //            index++;
    //        }
    //        list[firstIndex][0] = lastIndex;
    //        list[lastIndex][2] = firstIndex;
    //    }

    //    public int Shift()
    //    {
    //        if (readIndex == -1)
    //        {
    //            readIndex = lastIndex;
    //        }
    //        else
    //        {
    //            readIndex = list[readIndex][2];
    //        }
    //        return list[readIndex][1];
    //    }
    //}


    public class Deque<T>
    {
        private class Element
        {
            public T Value { get; set; }
            public Element Next { get; set; }
            public Element Prev { get; set; }

            public Element(T value) { Value = value; }
        }

        private Element First = null;
        private Element Last = null;

        public void Push(T value)
        {
            Element newElement = new Element(value);

            if ( Last != null)
            {
                Last.Next = newElement;
                newElement.Prev = Last;
            }

            //if (First == null)
            //{
            //    First = new Element(value);
            //}
            First ??= newElement;   //Si First es null, le asigna new element. Es lo mismo que lo comentado de encima pero simplificado

            Last = newElement;
            
        }
        public T Pop()
        {
            T value = Last.Value;
            Last = Last.Prev;

            return value;
        }
        public T Shift()
        {
            T value = First.Value;
            First = First.Next;

            return value;
        }
        public void Unshift(T value)
        {
            Element newValue = new Element(value);

            if (First != null)
            {
                First.Prev = newValue;
                newValue.Next = First;
            }

            Last ??= newValue;

            First = newValue;
        }

    }


    //------------------------------------------------------------------------------------------------------------------------------------------------

    public class CircularBuffer<T> //: Queue<T>
    {
        private int _writeIndex = 0;
        private int _readIndex = 0;
        private int _capacity;
        private (T,bool)[] _buffer;

        public CircularBuffer(int capacity)
        {
            _buffer = new (T,bool)[capacity];
            _capacity = capacity;
        }  

        public T Read(){
            T? result = _buffer[_readIndex].Item2 is not false ? _buffer[_readIndex].Item1 : throw new InvalidOperationException();
            _buffer[_readIndex].Item2 = false ;
            _readIndex = (_readIndex + 1) % _capacity;
            return result;
        } 

        public void Write(T value)
        {
            _buffer[_writeIndex] = _buffer[_writeIndex].Item2 is true ? throw new InvalidOperationException() : (value,true) ;
            _writeIndex = (_writeIndex + 1) % _capacity;
        }

        public void Overwrite(T value)
        {
            if(_writeIndex == _readIndex)
            {
                _readIndex = (_readIndex + 1)%_capacity;
            }
            _buffer[_writeIndex] = (value, true);
            _writeIndex = (_writeIndex + 1) % _capacity;
        }

        public void Clear()
        {
            _readIndex = 0;
            _writeIndex = 0;
            _buffer = new (T, bool)[_capacity];
        }
    }

    //  SOLUCIÓN COMUNIDAD --> Utiliza el tipo 
    //public class CircularBuffer<T> : Queue<T>
    //{
    //    private readonly int Capacity;

    //    public CircularBuffer(int capacity) => Capacity = capacity;
    //    public T Read() => Count == 0 ? throw new InvalidOperationException() : Dequeue();
    //    public void Write(T value)
    //    {
    //        if (Count == Capacity) throw new InvalidOperationException();
    //        Enqueue(value);
    //    }
    //    public void Overwrite(T value)
    //    {
    //        if (Count == Capacity) Dequeue();
    //        Enqueue(value);
    //    }
    //}
}


using System.ComponentModel.Design;
using System.Numerics;
using System.Security.Cryptography;
using Test;
class Exercise2
{
    
    public static void Main(string[] args)
    {

        //Location estaLocation = new Location("Santurdejo");

        //Console.WriteLine(estaLocation.Name);
        //estaLocation.Name = "Zaragoza";
        //Console.WriteLine(estaLocation.Name);

        //Sports Deportes = new Sports();
        //Console.WriteLine(Deportes);
        //Console.WriteLine(Deportes[1]);
        //Deportes[1] = "Furbo";
        //Console.WriteLine(Deportes[1]);
        //int x;

        //do
        //{
        //    x = Convert.ToInt32(Console.ReadLine());

        //    if (x != 0)
        //    {
        //        Console.WriteLine(Convert.ToString(x, 16));
        //        Console.WriteLine(Convert.ToString(x, 2));
        //    }
        //}
        //while (x != 0);

        //Triangle.IsScalene(3, 4, 3);

        //var dominoes = new[]
        //{
        //    (1, 2),
        //    (2, 3),
        //    (3, 1),
        //    (2, 4),
        //    (2, 4)
        //};
        //Dominoes.CanChain(dominoes);


        //var p = new BigInteger(7919);
        //var privateKeys = Enumerable.Range(0, 1000).Select(_ => DiffieHellman.PrivateKey(p)).ToArray();
        //var opopo = privateKeys.Distinct().Count();
        //opopo = privateKeys.Length - 100;
        //opopo = privateKeys.Length;

        //var msg = "TEITELHDVLSNHDTISEIIEA";
        //var sut = new RailFenceCipher(3);
        //string solution = sut.Decode(msg);

        //Appointment.HasPassed(DateTime.Now.AddYears(-1).AddHours(2));


        //var sut = new Meetup(9, 2013);
        //sut.Day(DayOfWeek.Monday, Schedule.Teenth);

        //var opopo = new AnagramTests();
        //opopo.Detects_two_anagrams();
    }

    public class Location
    {
        //private string locationName;

        public Location(string name) => Name = name;

        public string Name;
        //{
        //    get => locationName;
        //    set => locationName = value;
        //}
    }

    public class Sports
    {
        private string[] types = {"Baseball", "Basketball", "Football",
                               "Hockey", "Soccer", "Tennis",
                               "Volleyball" };

        public string this[int i]
        {
            get => types[i];
            set => types[i] = value;
        }
    }
}
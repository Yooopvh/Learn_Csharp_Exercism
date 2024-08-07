using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class Player
    {
        public int RollDie()
        {
            var random = new System.Random();
            return (int)random.Next(1, 19);

        }

        public double GenerateSpellStrength()
        {
            var random = new System.Random();
            return Math.Round((double)random.Next(1, 19),1);
        }
    }


    public static class DiffieHellman
    {
        public static BigInteger PrivateKey(BigInteger primeP)
        {
            var random = new System.Random();
            return random.Next(1, (int)primeP); //random.NextDouble() * (primeP-1) + 1;
        }

        public static BigInteger PublicKey(BigInteger primeP, BigInteger primeG, BigInteger privateKey)
        {
            BigInteger GpowA = 1;
            for (int i = 1;i <= privateKey; i++)
            {
              GpowA *= primeG;
            }
            return (GpowA % primeP);
        }

        public static BigInteger Secret(BigInteger primeP, BigInteger publicKey, BigInteger privateKey)
        {
            BigInteger GpowA = 1;
            for (int i = 1; i <= privateKey; i++)
            {
                GpowA *= publicKey;
            }
            return (GpowA % primeP);
        }
    }
}

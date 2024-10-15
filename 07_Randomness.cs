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

    //-----------------------------------------------------------------------------------------------------------------------------------------------

    public class SimpleCipher
    {
        private string _key = "";
        public SimpleCipher()
        {
            Random rnd= new Random();
            for (int i = 0; i < 100; i++)
            {
                _key += (char)(Math.Floor(rnd.NextDouble() * 26) + 'a');
            }
        }

        public SimpleCipher(string key) => _key = key;

        public string Key { get => _key; }

        public string Encode(string plaintext)
        {
            string result = "";
            int keyIndex = 0;
            foreach (char c in plaintext)
            {
                if (c < 'a' || c > 'z') throw new ArgumentException();  
                char keyChar = _key[keyIndex];
                char newChar = (char)(c + (keyChar - 'a'));
                char newLetter = newChar > 'z' ? (char) (newChar - 'z' + 'a'-1) : newChar;
                result += newLetter;

                keyIndex++;
                if (keyIndex == _key.Length) keyIndex = 0;
            }
            return result;
        }

        public string Decode(string ciphertext)
        {
            string result = "";
            int keyIndex = 0;
            foreach (char c in ciphertext)
            {
                if (c < 'a' || c > 'z') throw new ArgumentException();
                char keyChar = _key[keyIndex];
                char newChar = (char)(c - (keyChar - 'a'));
                char newLetter = newChar < 'a' ? (char)(newChar + 'z' - 'a'+1) : newChar;
                result += newLetter;

                keyIndex++;
                if (keyIndex == _key.Length) keyIndex = 0;
            }
            return result;
        }
    }
}

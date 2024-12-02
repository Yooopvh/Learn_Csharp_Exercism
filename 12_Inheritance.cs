using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Reactive;
using System.Reactive.Subjects;
using System.Security.Cryptography;
using System.Reactive.Linq;

namespace Test
{
    abstract class Character
    {
        private string _characterType = "";
        protected Character(string characterType)
        {
            _characterType = characterType;
        }

        public abstract int DamagePoints(Character target);

        public virtual bool Vulnerable()
        {
            return false;
        }

        public override string ToString()
        {
            return $"Character is a {_characterType}";  
        }
    }

    class Warrior : Character
    {
        public Warrior() : base("Warrior")
        {
        }

        public override int DamagePoints(Character target)
        {
            if (target.Vulnerable()) return 10; return 6;
        }
    }

    class Wizard : Character
    {
        private bool _spellPrepared ;
        private bool _vulnerable;
        public Wizard() : base("Wizard")
        {
            _spellPrepared = false;
            _vulnerable = true;
        }

        public override int DamagePoints(Character target)
        {
            if (_spellPrepared)  return 12; else return 3;
        }

        public void PrepareSpell()
        {
            _spellPrepared = true;
        }

        public override bool Vulnerable()
        {
            if (_spellPrepared)
            {
                return false;
            } else
            {
                return true;
            }
        }
    }

    //----------------------------------------------------------------------------------------------------------------------------------------------------



    //public class HangmanState : IObservable<Hangman>
    //{
    //    public string MaskedWord { get; }
    //    public ImmutableHashSet<char> GuessedChars { get; }
    //    public int RemainingGuesses { get; }

    //    public HangmanState(string maskedWord, ImmutableHashSet<char> guessedChars, int remainingGuesses)
    //    {
    //        MaskedWord = maskedWord;
    //        GuessedChars = guessedChars;
    //        RemainingGuesses = remainingGuesses;
    //    }

    //    public IDisposable Subscribe(IObserver<Hangman> observer)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

    //public class TooManyGuessesException : Exception
    //{
    //}

    //public class Hangman
    //{
    //    private string _word;
    //    public IObservable<HangmanState> StateObservable { get; }
    //    public IObserver<char> GuessObserver { get => throw new NotImplementedException("You need to implement this method."); }

    //    public Hangman(string word)
    //    {
    //        _word = word;
    //        //Devuelve un único state y luego finaliza
    //        StateObservable = Observable.Return<HangmanState>(new HangmanState(new string('_', word.Length), ImmutableHashSet<char>.Empty, 9));

    //        StateObservable.Subscribe(GuessObserver){

    //        }


    //    }

    //}
}

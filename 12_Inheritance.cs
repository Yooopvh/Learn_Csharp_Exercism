using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
}

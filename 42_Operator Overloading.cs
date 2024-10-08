using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Code
{
    public struct CurrencyAmount
    {
        private decimal amount;
        private string currency;

        public CurrencyAmount(decimal amount, string currency)
        {
            this.amount = amount;
            this.currency = currency;
        }

        // TODO: implement equality operators
        public static bool operator ==(CurrencyAmount lhs, CurrencyAmount rhs)
        {
            if (lhs.currency != rhs.currency) throw new ArgumentException();
            else return lhs.amount == rhs.amount;
        } 
        public static bool operator !=(CurrencyAmount lhs, CurrencyAmount rhs)
        {
            if (lhs.currency != rhs.currency) throw new ArgumentException();
            else return lhs.amount != rhs.amount;
        }
        // TODO: implement comparison operators
        public static bool operator >(CurrencyAmount lhs, CurrencyAmount rhs)
        {
            if (lhs.currency != rhs.currency) throw new ArgumentException();
            else return lhs.amount > rhs.amount;
        }
        public static bool operator <(CurrencyAmount lhs, CurrencyAmount rhs)
        {
            if (lhs.currency != rhs.currency) throw new ArgumentException();
            else return lhs.amount < rhs.amount;
        }
        // TODO: implement arithmetic operators

        public static CurrencyAmount operator +(CurrencyAmount lhs, CurrencyAmount rhs)
        {
            if (lhs.currency != rhs.currency) throw new ArgumentException();
            else return new CurrencyAmount(lhs.amount + rhs.amount,lhs.currency);
        }
        public static CurrencyAmount operator -(CurrencyAmount lhs, CurrencyAmount rhs)
        {
            if (lhs.currency != rhs.currency) throw new ArgumentException();
            else return new CurrencyAmount(lhs.amount - rhs.amount, lhs.currency);
        }
        public static CurrencyAmount operator *(CurrencyAmount lhs, CurrencyAmount rhs)
        {
            if (lhs.currency != rhs.currency) throw new ArgumentException();
            else return new CurrencyAmount(lhs.amount * rhs.amount, lhs.currency);
        }
        public static CurrencyAmount operator /(CurrencyAmount lhs, CurrencyAmount rhs)
        {
            if (lhs.currency != rhs.currency) throw new ArgumentException();
            else return new CurrencyAmount(lhs.amount / rhs.amount, lhs.currency);
        }

        // TODO: implement type conversion operators
        public static explicit operator double(CurrencyAmount lhs) =>  (double) lhs.amount;
        public static implicit operator decimal(CurrencyAmount lhs) => lhs.amount;

    }
}

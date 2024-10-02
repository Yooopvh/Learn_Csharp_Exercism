using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code
{
    public enum States
    {
        Closed,
        TransactionStarted,
        DataWritten,
        Invalid
    }
    public class Orm : IDisposable
    {
        private Database database;

        public Orm(Database database)
        {
            this.database = database;
        }

        public void Begin() 
        {
            if (database.State == States.Closed)
            {
                database.State = States.TransactionStarted;
            }else
            {
                Dispose();
                throw new InvalidOperationException();
            }
        }

        public void Write(string data)
        {
            if (database.State == States.TransactionStarted)
            {
                database.State = States.DataWritten;
            }
            else
            {
                Dispose();
                throw new InvalidOperationException();
            }
        }

        public void Commit()
        {
            if (database.State == States.DataWritten)
            {
                database.State = States.Closed;
            }
            else
            {
                Dispose();
                throw new InvalidOperationException();
            }
        }

        public void Dispose()
        {
            database.State = States.Closed;
        }
    }

    public class Database
    {
        public States State = States.Closed;
        
    }
}

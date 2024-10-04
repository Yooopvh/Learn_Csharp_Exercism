using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code
{
    public class Orm
    {
        private Database database;

        public Orm(Database database)
        {
            this.database = database;
        }

        public void Write(string data)
        {
            using (database)
            {
                database.BeginTransaction();
                Console.WriteLine("Transaction Started");
                database.Write(data);
                Console.WriteLine("Data Written");
                database.EndTransaction();
                Console.WriteLine("Transaction Ended");
            }
                

        }

        public bool WriteSafely(string data)
        {
            try
            {
                database.BeginTransaction();
                database.Write(data);
                database.EndTransaction();
                return true;
            } catch (Exception ex)
            {
                return false;
            }
        }
    }
}

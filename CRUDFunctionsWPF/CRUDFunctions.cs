using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDFunctionsWPF
{
    public class CRUDFunctions
    {

        private string _connectionString;

        //Use this to set the connection in other solutions.
        public void SetConnectionString(string connectionString)
        {
            _connectionString = connectionString;
        }

        //Inserting data into a database.
        public void Insert(string name, int age, string gender)
        {
            //Based on provided connection string, can be used on multiple dbs.
            using (var context = new CRUDContext(_connectionString))
            {

                var newPerson = new Person
                {
                    Name = name,
                    Age = age,
                    Gender = gender
                };

                context.Person.Add(newPerson);
                context.SaveChanges();

            }



        }




    }
}

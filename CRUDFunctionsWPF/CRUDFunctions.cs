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

        //Deleting person data based on ID
        public bool Delete(int id) 
        {

            using (var context = new CRUDContext(_connectionString))
            {
                var personID = context.Person.Find(id);

                if (personID != null)
                {
                    context.Person.Remove(personID);
                    context.SaveChanges();
                    return true;
                }
            }
            return false;



        }

        public void Update(int id, string name, int age, string gender)
        {

            using (var context = new CRUDContext(_connectionString))
            {
                var personID = context.Person.Find(id);

                if (personID != null)
                {
                    
                    personID.Name = name;
                    personID.Age = age;
                    personID.Gender = gender;

                    context.Update(personID);
                    context.SaveChanges();
                    
                }

            }

        }




    }
}

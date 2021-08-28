using Dapper;
using System;
using System.Data;
using System.Data.SqlClient;
using Bol;

namespace DalUsingDapper
{
    public class DatabaseHealper
    {
        private string ConnectionString { get; set; }
        public DatabaseHealper(string con)
        {
            ConnectionString = con;
        }

        public Person Get_Data(int Id)
        {
            Person persons = new Person();
            var sql = "Get_Persons";
            var param = new DynamicParameters();
            using(var connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();
                param.Add("@Id", Id);
                var data = connection.QueryFirst(sql, param, commandType: CommandType.StoredProcedure);

                persons.Id = Convert.ToInt32(data.Id);
                persons.FirstName = Convert.ToString(data.FirstName);
                persons.LastName = Convert.ToString(data.LastName);
                persons.Email = Convert.ToString(data.Email);

            }
            return persons;
        }

        public bool Insert(Person model)
        {
            var data=0;
            var sql = "Inset_Persons";
            var param = new DynamicParameters();

            using(var connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();
                param.Add("@FirstName", model.FirstName);
                param.Add("@LastName", model.LastName);
                param.Add("@Email", model.Email);
                data = connection.Execute(sql, param, commandType: CommandType.StoredProcedure);
            }
            return data > 0 ? true : false;                      
        }

        public bool Update(Person model)
        {
            var data = 0;
            var sql = "Update_Person";
            var param = new DynamicParameters();

            using(var connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();
                param.Add("@Id", model.Id);
                param.Add("@FirstName", model.FirstName);
                param.Add("@LastName", model.LastName);
                param.Add("@Email", model.Email);
                data = connection.Execute(sql, param, commandType: CommandType.StoredProcedure);
            }
            return data > 0 ? true : false;
        }

        public bool Delete(int Id)
        {
            var data = 0;
            var sql = "Delete_Persons";
            var param = new DynamicParameters();
            using (var connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();
                param.Add("@Id", Id);
                data = connection.Execute(sql, param, commandType: CommandType.StoredProcedure);  
            }
            return data > 0 ? true : false;
        }
    }
}

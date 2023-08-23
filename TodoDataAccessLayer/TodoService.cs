using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoDataAccessLayer
{
    public class TodoService
    {
        private string _connectionString = @"Data Source=STEVEBSTORM\MSSQLSERVER01;Initial Catalog=TodoDEMOTechni;Integrated Security=True;";

        public void Create(Todo todo)
        {
            using(SqlConnection cnx = new SqlConnection(_connectionString))
            {
                using(SqlCommand cmd = cnx.CreateCommand())
                {
                    string sql = "INSERT INTO Todo (Nom, DateLimite, Description, Priorite) " +
                        "VALUES (@nom, @date, @desc, @prio)";

                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("nom", todo.Nom);
                    cmd.Parameters.AddWithValue("date", todo.DateLimite);
                    cmd.Parameters.AddWithValue("desc", todo.Description);
                    cmd.Parameters.AddWithValue("prio", todo.Priorite);

                    cnx.Open();
                    cmd.ExecuteNonQuery();
                    cnx.Close();
                }
            }
        }

        public List<Todo> GetAll()
        {
            List<Todo> list = new List<Todo>();
            using(SqlConnection cnx = new SqlConnection(_connectionString)) 
            {
                using (SqlCommand cmd = cnx.CreateCommand())
                {
                    string sql = "SELECT * FROM Todo";
                    cmd.CommandText = sql;

                    cnx.Open();
                    using (SqlDataReader r = cmd.ExecuteReader()) 
                    {
                        while(r.Read())
                        {
                            list.Add(new Todo
                            {
                                Id = (int)r["Id"],
                                Nom = (string)r["Nom"],
                                Description = (string)r["Description"],
                                DateLimite = (DateTime)r["DateLimite"],
                                Priorite = (int)r["Priorite"],
                                IsFinished = (bool)r["IsFinished"]
                            });
                        }
                    }
                    cnx.Close();
                }
            }
            return list;
        }
    }
}

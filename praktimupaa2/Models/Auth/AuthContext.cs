using Npgsql;
using praktimupaa2.Models;
using praktimupaa2.Helpers;
using praktimupaa2.Models.Login;

namespace praktimupaa2.Models.Auth
{
    public class AuthContext
    {
        private readonly string _constr;
      
        private string _errorMsg;
        public AuthContext(string connString)
        {
            _constr = connString;
        }


        public List<Login.Auth> Authentifikasi(string namaUser, string password, IConfiguration _config)
        {
            List<Login.Auth> persons = new List<Login.Auth>();
            string query = string.Format(@"SELECT  nama ,password FROM person
                                            where person.nama ='{0}' and person.password ='{1}'", namaUser, password);
            postgresHelper db = new postgresHelper(this._constr);
            try
            {
                NpgsqlCommand cmd = db.GetNpgsqlCommand(query);
                NpgsqlDataReader reader = cmd.ExecuteReader();
                JwtHelper jwtHelper = new JwtHelper(_config);
                
                while (reader.Read())
                {
                    persons.Add(new Login.Auth()
                    {  
                        name = reader["nama"].ToString(),
                        password = reader["password"].ToString()
                    });
                }
            }
            catch (Exception ex)
            {
                _errorMsg = ex.Message;
            }
            return persons;
        }



        public Person.Person Auth(string nama, string password)
        {
            Person.Person person = null;

            string query = "SELECT id_person, nama, password FROM person WHERE password ILIKE @password;";

            
            postgresHelper db = new postgresHelper(_constr);
            
                try
                {
                    using (NpgsqlCommand cmd = db.GetNpgsqlCommand(query))
                    {
                        cmd.Parameters.AddWithValue("@nama", nama);
                        cmd.Parameters.AddWithValue("@password", password);

                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read()) // Jika ada hasil dari query
                            {
                                person = new Person.Person()
                                {
                                    id_person = reader.GetInt32(0),
                                    nama = reader.GetString(2)
                                };
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    _errorMsg = ex.Message;
                }
                finally
                {
                    db.closeConnection();
                }
            

            return person;
        }

        public bool RegisterPerson(Person.Person person)
        {
           
            if (person == null)
            {
                throw new ArgumentNullException(nameof(person), "Person object cannot be null.");
            }


            bool result = false;

            string query = @"INSERT INTO person (nama, password) 
                     VALUES (@nama, @password)";

            try
            {
            
                postgresHelper db = new postgresHelper(this._constr);
                using (NpgsqlCommand cmd = db.GetNpgsqlCommand(query))
                {
                    cmd.Parameters.AddWithValue("@nama", person.nama);
                    cmd.Parameters.AddWithValue("@password", person.password);
                    cmd.ExecuteNonQuery();
                }
                    
                    result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in RegisterPerson: " + ex.ToString());
            }

            return result;
        }



    }
}

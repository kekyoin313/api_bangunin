using Npgsql;
using praktimupaa2.Helpers;

namespace praktimupaa2.Models.Satuan
{
    public class SatuanContext
    {
        private readonly string _connString;
        private string _errorMessage;

        public SatuanContext(string connString)
        {
            _connString = connString;
        }

        public List<Satuan> GetAllSatuans()
        {
            List<Satuan> satuans = new List<Satuan>();
            string query = "SELECT * FROM satuan";
            postgresHelper helper = new postgresHelper(_connString);
            try
            {
                NpgsqlCommand cmd = helper.GetNpgsqlCommand(query);
                NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Satuan satuan = new Satuan();
                    satuan.id_satuan = reader.GetInt32(0);
                    satuan.nama_satuan = reader.GetString(1);
                    satuans.Add(satuan);
                }
                cmd.Dispose();
                helper.closeConnection();
            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
            }
            return satuans;
        }


        public bool AddSatuan(Satuan satuan)
        {
            string query = "INSERT INTO satuan (nama_satuan) VALUES (@nama)";
            postgresHelper helper = new postgresHelper(_connString);
            try
            {
                NpgsqlCommand cmd = helper.GetNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("@nama", satuan.nama_satuan);               
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                helper.closeConnection();
                return true;
            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
                return false;
            }
        }
        public string ErrorMessage => _errorMessage;
        public bool UpdateSatuan(Satuan satuan)
        {
            string query = "UPDATE satuan SET nama_satuan = @nama WHERE id_satuan = @id";
            postgresHelper helper = new postgresHelper(_connString);
            try
            {
                NpgsqlCommand cmd = helper.GetNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("@id", satuan.id_satuan);
                cmd.Parameters.AddWithValue("@nama", satuan.nama_satuan);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                helper.closeConnection();
                return true;
            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
                return false;
            }
        }

        public bool DeleteSatuan(int id)
        {
            string query = "DELETE FROM satuan WHERE id_satuan = @id";
            postgresHelper helper = new postgresHelper(_connString);
            try
            {
                NpgsqlCommand cmd = helper.GetNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                helper.closeConnection();
                return true;
            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
                return false;
            }
        }
    }
}

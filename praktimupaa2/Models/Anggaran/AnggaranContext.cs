using Npgsql;
using praktimupaa2.Helpers;

namespace praktimupaa2.Models.Anggaran
{
    public class AnggaranContext
    {
        private readonly string _connString;
        private string _errorMessage;

        public AnggaranContext(string connString)
        {
            _connString = connString;
        }

        public List<Anggaran> GetAllAnggarans()
        {
            List<Anggaran> anggarans = new List<Anggaran>();
            string query = "SELECT * FROM anggaran";
            postgresHelper helper = new postgresHelper(_connString);
            try
            {
                NpgsqlCommand cmd = helper.GetNpgsqlCommand(query);
                NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Anggaran anggaran = new Anggaran();
                    anggaran.id_anggaran = reader.GetInt32(0);
                    anggaran.total_anggaran = reader.GetInt32(1);
                    anggaran.id_tipe_anggaran = reader.IsDBNull(2) ? (int?)null : reader.GetInt32(2);
                    anggarans.Add(anggaran);
                }
                cmd.Dispose();
                helper.closeConnection();
            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
            }
            return anggarans;
        }

        public Anggaran GetAnggaranById(int id)
        {
            Anggaran anggaran = null;
            string query = "SELECT * FROM anggaran WHERE id_anggaran = @id";
            postgresHelper helper = new postgresHelper(_connString);
            try
            {
                NpgsqlCommand cmd = helper.GetNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("@id", id);
                NpgsqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    anggaran = new Anggaran();
                    anggaran.id_anggaran = reader.GetInt32(0);
                    anggaran.total_anggaran = reader.GetInt32(1);
                    anggaran.id_tipe_anggaran = reader.IsDBNull(2) ? (int?)null : reader.GetInt32(2);
                }
                cmd.Dispose();
                helper.closeConnection();
            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
            }
            return anggaran;
        }

        public bool AddAnggaran(Anggaran anggaran)
        {
            string query = "INSERT INTO anggaran (total_anggaran,id_tipe_anggaran) VALUES (@total,@tipe)";
            postgresHelper helper = new postgresHelper(_connString);
            try
            {
                NpgsqlCommand cmd = helper.GetNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("@total", anggaran.total_anggaran);
                cmd.Parameters.AddWithValue("@tipe", (object?)anggaran.id_tipe_anggaran ?? DBNull.Value);
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

        public bool UpdateAnggaran(Anggaran anggaran)
        {                                           
            string query = "UPDATE anggaran SET total_anggaran = @total, id_tipe_anggaran = @tipe WHERE id_anggaran = @id";
            postgresHelper helper = new postgresHelper(_connString);
            try
            {
                NpgsqlCommand cmd = helper.GetNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("@id", anggaran.id_anggaran);
                cmd.Parameters.AddWithValue("@total", anggaran.total_anggaran);
                cmd.Parameters.AddWithValue("@tipe", (object?)anggaran.id_tipe_anggaran ?? DBNull.Value);
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

        public bool DeleteAnggaran(int id)
        {
            string query = "DELETE FROM anggaran WHERE id_anggaran = @id";
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

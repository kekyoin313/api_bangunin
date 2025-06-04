using Npgsql;
using praktimupaa2.Helpers;

namespace praktimupaa2.Models.Tipe_anggaran
{
    public class Tipe_anggaranContext
    {
        private readonly string _connString;
        private string _errorMessage;

        public Tipe_anggaranContext(string connString)
        {
            _connString = connString;
        }

        public List<Tipe_anggaran> GetAllTipe_anggarans()
        {
            List<Tipe_anggaran> tipe_anggarans = new List<Tipe_anggaran>();
            string query = "SELECT * FROM tipe_anggaran";
            postgresHelper helper = new postgresHelper(_connString);
            try
            {
                NpgsqlCommand cmd = helper.GetNpgsqlCommand(query);
                NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Tipe_anggaran tipe_anggaran = new Tipe_anggaran();
                    tipe_anggaran.id_tipe_anggaran = reader.GetInt32(0);
                    tipe_anggaran.tipe = reader.GetString(1);
                    tipe_anggarans.Add(tipe_anggaran);
                }
                cmd.Dispose();
                helper.closeConnection();
            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
            }
            return tipe_anggarans;
        }

        //public Tipe_anggaran GetTipe_anggaranById(int id)
        //{
        //    Tipe_anggaran tipe_anggaran = null;
        //    string query = "SELECT * FROM tipe_anggaran WHERE id_tipe_anggaran = @id";
        //    postgresHelper helper = new postgresHelper(_connString);
        //    try
        //    {
        //        NpgsqlCommand cmd = helper.GetNpgsqlCommand(query);
        //        cmd.Parameters.AddWithValue("@id", id);
        //        NpgsqlDataReader reader = cmd.ExecuteReader();
        //        if (reader.Read())
        //        {
        //            tipe_anggaran = new Tipe_anggaran();
        //            tipe_anggaran.id_tipe_anggaran = reader.GetInt32(0);
        //            tipe_anggaran.tipe = reader.GetString(1);
        //        }
        //        cmd.Dispose();
        //        helper.closeConnection();
        //    }
        //    catch (Exception ex)
        //    {
        //        _errorMessage = ex.Message;
        //    }
        //    return tipe_anggaran;
        //}

        public bool AddTipe_anggaran(Tipe_anggaran tipe_anggaran)
        {
            string query = "INSERT INTO tipe_anggaran (tipe) VALUES (@tipe)";
            postgresHelper helper = new postgresHelper(_connString);
            try
            {
                NpgsqlCommand cmd = helper.GetNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("@tipe", tipe_anggaran.tipe);
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

        public bool UpdateTipe_anggaran(Tipe_anggaran tipe_anggaran)
        {
            string query = "UPDATE tipe_anggaran SET tipe = @tipe WHERE id_tipe_anggaran = @id";
            postgresHelper helper = new postgresHelper(_connString);
            try
            {
                NpgsqlCommand cmd = helper.GetNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("@id", tipe_anggaran.id_tipe_anggaran);
                cmd.Parameters.AddWithValue("@tipe", tipe_anggaran.tipe);
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

        public bool DeleteTipe_anggaran(int id)
        {
            string query = "DELETE FROM tipe_anggaran WHERE id_tipe_anggaran = @id";
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

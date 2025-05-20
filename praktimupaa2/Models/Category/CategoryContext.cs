using Npgsql;
using praktimupaa2.Helpers;
using System.Data;

namespace praktimupaa2.Models.Category
{
    public class CategoryContext
    {
        private readonly string _connString;
        private string _errorMessage;

        public CategoryContext(string connString)
        {
            _connString = connString;
        }

        public List<Category> GetAllCategories()
        {
            List<Category> categories = new List<Category>();
            string query = "SELECT * FROM category";
            postgresHelper helper = new postgresHelper(_connString);
            try
            {
                NpgsqlCommand cmd = helper.GetNpgsqlCommand(query);
                NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Category category = new Category();
                    category.id_category = reader.GetInt32(0);
                    category.nama_category = reader.GetString(1);
                    categories.Add(category);

                }
                cmd.Dispose();
                helper.closeConnection();
            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
            }
            return categories;
        }

        public bool AddCategory(Category category)
        {
            string query = "INSERT INTO category (nama_category) VALUES (@nama)";
            postgresHelper helper = new postgresHelper(_connString);
            try
            {
                NpgsqlCommand cmd = helper.GetNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("@nama", category.nama_category);
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

        public bool UpdateCategory(Category category)
        {
            string query = "UPDATE category SET nama_category = @nama WHERE id_category = @id";
            postgresHelper helper = new postgresHelper(_connString);
            try
            {
                NpgsqlCommand cmd = helper.GetNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("@id", category.id_category);
                cmd.Parameters.AddWithValue("@nama", category.nama_category);
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

        public bool DeleteCategory(int id)
        {
            string query = "DELETE FROM category WHERE id_category = @id";
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

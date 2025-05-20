using Npgsql;
using praktimupaa2.Helpers;

namespace praktimupaa2.Models.Product
{
    public class ProductContext
    {
        private readonly string _connString;
        private string _errorMessage;

        public ProductContext(string connString)
        {
            _connString = connString;
        }

        public List<Product> GetAllProducts()
        {
            List<Product> products = new List<Product>();
            string query = "SELECT * FROM produk";
            postgresHelper helper = new postgresHelper(_connString);
            try
            {
                NpgsqlCommand cmd = helper.GetNpgsqlCommand(query);
                NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Product product = new Product();
                    product.id_product = reader.GetInt32(0);
                    product.nama_product = reader.GetString(1);
                    product.harga = reader.GetInt32(2);
                    product.stok = reader.GetInt32(3);
                    product.id_category = reader.IsDBNull(4) ? (int?)null : reader.GetInt32(4);
                    products.Add(product);
                }
                cmd.Dispose();
                helper.closeConnection();
            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
            }
            return products;
        }

        public Product GetProductById(int id)
        {
            Product product = null;
            string query = "SELECT * FROM produk WHERE id_produk = @id";
            postgresHelper helper = new postgresHelper(_connString);
            try
            {
                NpgsqlCommand cmd = helper.GetNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("@id", id);
                NpgsqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    product = new Product();
                    product.id_product = reader.GetInt32(0);
                    product.nama_product = reader.GetString(1);
                    product.harga = reader.GetInt32(2);
                    product.stok = reader.GetInt32(3);
                    product.id_category = reader.IsDBNull(4) ? (int?)null : reader.GetInt32(4);
                }
                cmd.Dispose();
                helper.closeConnection();
            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
            }
            return product;
        }

        public bool AddProduct(Product product)
        {
            string query = "INSERT INTO produk (nama_produk, harga, stok, id_category) VALUES (@nama, @harga, @stok, @category)";
            postgresHelper helper = new postgresHelper(_connString);
            try
            {
                NpgsqlCommand cmd = helper.GetNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("@nama", product.nama_product);
                cmd.Parameters.AddWithValue("@harga", product.harga);
                cmd.Parameters.AddWithValue("@stok", product.stok);
                cmd.Parameters.AddWithValue("@category", (object?)product.id_category ?? DBNull.Value);
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

        public bool UpdateProduct(Product product)
        {
            string query = "UPDATE produk SET nama_produk = @nama, harga = @harga, stok = @stok, id_category = @category WHERE id_produk = @id";
            postgresHelper helper = new postgresHelper(_connString);
            try
            {
                NpgsqlCommand cmd = helper.GetNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("@id", product.id_product);
                cmd.Parameters.AddWithValue("@nama", product.nama_product);
                cmd.Parameters.AddWithValue("@harga", product.harga);
                cmd.Parameters.AddWithValue("@stok", product.stok);
                cmd.Parameters.AddWithValue("@category", (object?)product.id_category ?? DBNull.Value);
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

        public bool DeleteProduct(int id)
        {
            string query = "DELETE FROM produk WHERE id_produk = @id";
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

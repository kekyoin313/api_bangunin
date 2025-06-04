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
            string query = "SELECT * FROM product";
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
                    product.id_satuan = reader.IsDBNull(5) ? (int?)null : reader.GetInt32(5);
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
            string query = "SELECT * FROM product WHERE id_product = @id";
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
                    product.id_satuan = reader.IsDBNull(5) ? (int?)null : reader.GetInt32(5);
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
            string query = "INSERT INTO product (nama_product, harga, stok, id_category, id_satuan) VALUES (@nama, @harga, @stok, @category, @satuan)";
            postgresHelper helper = new postgresHelper(_connString);
            try
            {
                NpgsqlCommand cmd = helper.GetNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("@nama", product.nama_product);
                cmd.Parameters.AddWithValue("@harga", product.harga);
                cmd.Parameters.AddWithValue("@stok", product.stok);
                cmd.Parameters.AddWithValue("@category", (object?)product.id_category ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@satuan", (object?)product.id_satuan ?? DBNull.Value);
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
        public bool UpdateProduct(Product product)
        {
            string query = "UPDATE product SET nama_product = @nama, harga = @harga, stok = @stok, id_category = @category, id_satuan = @satuan WHERE id_product = @id";
            postgresHelper helper = new postgresHelper(_connString);
            try
            {
                NpgsqlCommand cmd = helper.GetNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("@id", product.id_product);
                cmd.Parameters.AddWithValue("@nama", product.nama_product);
                cmd.Parameters.AddWithValue("@harga", product.harga);
                cmd.Parameters.AddWithValue("@stok", product.stok);
                cmd.Parameters.AddWithValue("@category", (object?)product.id_category ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@satuan", (object?)product.id_satuan ?? DBNull.Value);
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
            string query = "DELETE FROM product WHERE id_product = @id";
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

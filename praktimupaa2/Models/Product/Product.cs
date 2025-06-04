namespace praktimupaa2.Models.Product
{
    public class Product
    {
        public int id_product { get; set; }
        public string nama_product { get; set; }
        public int harga { get; set; }
        public int stok { get; set; }
        public int? id_category { get; set; }
        public int? id_satuan { get; set; }
    }
}

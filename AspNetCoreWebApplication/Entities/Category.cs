using System.ComponentModel.DataAnnotations;
namespace AspNetCoreWebApplication.Entities
{
    public class Category
    {

        public int Id { get; set; }
        [Display(Name = "Kategori Adı"), Required(ErrorMessage = "Kategori Adı Boş Geçilemez")]
        public string Name { get; set; }
        [Display(Name = "Kategori Açıklama")]
        public string? Description { get; set; }
        [Display(Name = "Kategori Resmi"), StringLength(50)]
        public string? Image { get; set; }
        [Display(Name = "Eklenme Tarihi"),ScaffoldColumn(false)]
        public DateTime? CreateDate { get; set; } = DateTime.Now;

        public ICollection<Product> Products { get; set; }
        public Category()
        {
            Products = new List<Product>();
        }
    }
}

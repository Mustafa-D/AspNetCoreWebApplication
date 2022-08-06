using System.ComponentModel.DataAnnotations;

namespace AspNetCoreWebApplication.Entities
{
    public class Product
    {
        public int Id { get; set; }
        [Display(Name = "Ürün Adı"), Required(ErrorMessage = "Ürün Adı Boş Geçilemez")]
        public string Name { get; set; }
        [Display(Name = "Kategori Açıklama")]
        public string? Description { get; set; }
        [Display(Name = "Kategori Resmi"), StringLength(50)]
        public string? Image { get; set; }
        [Display(Name = "Eklenme Tarihi")]
        public DateTime? CreateDate { get; set; } = DateTime.Now;
        public int CategoryId { get; set; }
        public virtual Category? Category { get; set; }

        public int BrandId { get; set; }
        public virtual Brand? Brand { get; set; }
    }
}

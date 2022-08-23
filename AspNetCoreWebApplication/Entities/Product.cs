using System.ComponentModel.DataAnnotations;

namespace AspNetCoreWebApplication.Entities
{
    public class Product
    {
        public int Id { get; set; }
        [Display(Name = "Ürün Adı"), Required(ErrorMessage = "Ürün Adı Boş Geçilemez")]
        public string Name { get; set; }
        [Display(Name = "Ürün Açıklama")]
        public string? Description { get; set; }
        [Display(Name = "Ürün Resmi"), StringLength(50)]
        public string? Image { get; set; }
        [Display(Name = "Eklenme Tarihi"),ScaffoldColumn(false)]
        public DateTime? CreateDate { get; set; } = DateTime.Now;
        [Display(Name ="Ürün Kategorisi")]
        public int CategoryId { get; set; }
        [Display(Name = "Ürün Kategorisi")]
        public virtual Category? Category { get; set; }
        [Display(Name ="Ürün Markası")]

        public int BrandId { get; set; }
        [Display(Name = "Ürün Markası")]
        public virtual Brand? Brand { get; set; }
    }
}

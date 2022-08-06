using System.ComponentModel.DataAnnotations;


namespace AspNetCoreWebApplication.Entities
{
    public class Brand
    {
        public int Id { get; set; }
        [Display(Name="Marka Adı"),Required(ErrorMessage ="Marka Adı Boş Geçilemez")]
        public string Name { get; set; }
        [Display(Name = "Marka Açıklama")]
        public string?  Description { get; set; }
        [Display(Name = "Marka Logosu"), StringLength(50)]
        public string? Logo { get; set; }
        public ICollection<Product> Products { get; set; }
        public Brand()
        {
            Products = new List<Product>();
        }
    }
}

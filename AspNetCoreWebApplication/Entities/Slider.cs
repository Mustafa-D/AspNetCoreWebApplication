﻿using System.ComponentModel.DataAnnotations;

namespace AspNetCoreWebApplication.Entities
{
    public class Slider
    {
        public int Id { get; set; }
        
        [Display(Name = "Kategori Resmi"), StringLength(50)]
        public string? Image { get; set; }
        
    }
}

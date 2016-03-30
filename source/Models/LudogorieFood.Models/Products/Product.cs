﻿namespace LudogorieFood.Models.Products
{
    using System.ComponentModel.DataAnnotations;

    using Web.Common.Models;

    using Types;

    public class Product : BaseModel<int>
    {
        public Product()
        {
            this.InStock = true;
        }

        [Required]
        public string Name { get; set; }

        public string PictureName { get; set; }

        [Required]
        public MeasureType Measure { get; set; }

        [Required]
        public decimal Price { get; set; }

        public decimal NewPrice { get; set; }

        public bool InStock { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

    }
}

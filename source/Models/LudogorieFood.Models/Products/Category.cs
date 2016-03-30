namespace LudogorieFood.Models.Products
{
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;

    using LudogorieFood.Web.Common.Models;

    public class Category : BaseModel<int>
    {
        public Category()
        {
            this.Products = new HashSet<Product>();
        }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}

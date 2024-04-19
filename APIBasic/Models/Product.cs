using System.Diagnostics;
using System.Text.Json.Serialization;

namespace APIBasic.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Sku { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public bool IsAvailable { get; set;}

        //"fk" so to speak
        public int CategoryId { get; set; }

        [JsonIgnore] 
        public virtual Category Category { get; set; }

        //each product has a category
    }
}

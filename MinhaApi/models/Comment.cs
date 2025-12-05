using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MinhaApi.models
{
    public class Comment
    {

        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public int? StockId { get; set; }
        public Stock? Stock { get; set; }

        // public int Id { get; set; }
        // public string Symbol { get; set; } = string.Empty;

        // public string CompanyName { get; set; } =string.Empty;
        // [Column(TypeName = "decimal(18,2)")]
        // public decimal Purchase { get; set; }

        // [Column(TypeName = "decimal(18,2)")]
        // public decimal LastDiv { get; set; }

        // public string Industry { get; set; } = string.Empty;

        // public long MarketCap { get; set; }

        // public List<Comment> Comments { get; set; } = new List<Comment>(); // 
    }
}
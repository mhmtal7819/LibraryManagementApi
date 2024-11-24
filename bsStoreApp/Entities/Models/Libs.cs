using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Libs
    {
        public int Id { get; set; }
        public string Title { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
        // Ref: navigation property
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}

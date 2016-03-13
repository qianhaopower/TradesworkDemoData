using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataService.Models
{
    public class ComponentItem
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string Comment { get; set; }
        public bool MarketTag { get; set; }
        public int PropertyComponentId { get; set; }



        public virtual PropertyComponent Component { get; set; }
    }
}

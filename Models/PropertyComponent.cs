using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace DataService.Models
{
    public class PropertyComponent
    {

        public PropertyComponent()
        {
            Items = new List<ComponentItem>();
        }
        [Key]
        public int Id { get; set; }
        public string description { get; set; }
        public int ClientId { get; set; }

        public virtual Client Client { get; set; }


   
        public virtual List<ComponentItem> Items { get; set; }
    }
}

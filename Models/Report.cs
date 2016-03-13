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
  public   class Report
    {
        [Key]
        public int Id { get; set; }


        public string JsonContent { get; set; }

        public DateTime CreatedDate { get; set; }
      
        public virtual Client ClientEntity { get; set; }
    }
}

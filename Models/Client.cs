
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace DataService.Models
{
    public class Client
    {

        public Client()
        {
            Components = new List<PropertyComponent>();
        }
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }



        [Required]
        public virtual Report ClientReport { get; set; }
        public virtual List<PropertyComponent> Components { get; set; }
    }
}
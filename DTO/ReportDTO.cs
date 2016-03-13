using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CentralDataService.DTO
{
    public class ReportDTO
    {

        public int ReportId { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
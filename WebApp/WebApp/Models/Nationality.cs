using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class Nationality
    {
        public int Id { get; set; }
        [StringLength(maximumLength: 100)]
        public string NameAr { get; set; }
        [StringLength(maximumLength: 100)]
        public string NameEn { get; set; }
        public int Order { get; set; }

    }
}

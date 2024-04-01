using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class DayName
    {
        public int Id { get; set; }
        [StringLength(30)]
        public string Name { get; set; }

        [StringLength(30)]
        public string NameEn { get; set; }
        [StringLength(30)]
        public string Language { get; set; }
    }
}

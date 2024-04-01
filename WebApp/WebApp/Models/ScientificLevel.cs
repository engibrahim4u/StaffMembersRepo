using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class ScientificLevel
    {
        public int Id { get; set; }

        [StringLength(maximumLength: 25)]
        public string NameEn { get; set; }

        [StringLength(maximumLength: 25)]
        public string Name { get; set; }
        public int Order { get; set; }
    }
}

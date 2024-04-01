using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class Place
    {
        public int Id { get; set; }
        [StringLength(maximumLength: 200)]
        public string Name { get; set; }
    }
}

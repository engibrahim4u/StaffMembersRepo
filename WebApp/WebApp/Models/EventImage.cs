using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class EventImage
    {
        public int Id { get; set; }
        public int Category { get; set; }
        public string FileName { get; set; }
        public int Order { get; set; }
        public int EventId { get; set; }
    }
}

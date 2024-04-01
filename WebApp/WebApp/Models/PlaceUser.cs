using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class PlaceUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public int PlaceId { get; set; }
        public Place Place { get; set; }
    }
}

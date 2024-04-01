using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.ViewModels
{
    public class UserPlaceListVM
    {
        public UserPlaceListVM()
        {
            Users = new List<string>();
        }
        public int PlaceId { get; set; }
        public string PlaceName { get; set; }
        public List<string> Users { get; set; }
    }
}

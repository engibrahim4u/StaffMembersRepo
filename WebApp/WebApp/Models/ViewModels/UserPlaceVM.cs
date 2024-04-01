using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.ViewModels
{
    public class UserPlaceVM
    {
        public string UserId { get; set; }
        public string UserName { get; set; }

        public string Name { get; set; }

        public bool IsSelected { get; set; }
    }
}

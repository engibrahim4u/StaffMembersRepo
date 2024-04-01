using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.ViewModels
{
    public class HomeVM
    {
        public NumbersVM Numbers { get; set; }
        public List<EventsVM> Events { get; set; }
        public List<EventsVM> EventActivities { get; set; }

        public List<NewsVM> News { get; set; }

    }
}

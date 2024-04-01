using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Helper
{
    public class MyTimeZone
    {
        public static DateTime Now => DateTime.UtcNow.AddHours(2);

    }
}

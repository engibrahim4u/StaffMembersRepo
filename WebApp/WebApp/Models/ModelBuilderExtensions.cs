using WebApp.Helper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Setting>().HasData(
                new Setting
                {
                    Id = 1,
                    Available = false,
                    NotAvailableMessage = "الموقع غير متاح",
                    CurrentAcademicYear = "2021/2020",
                    StartDate = Convert.ToDateTime("1-1-2020"),
                    EndDate = Convert.ToDateTime("1-5-2022")
                }
                );
            modelBuilder.Entity<DayName>().HasData(
                new DayName { Id = 1, Name = "السبت", Language = "Arabic", NameEn = "Saturday" },
                new DayName { Id = 2, Name = "الأحد", Language = "Arabic", NameEn = "Sunday" },
                new DayName { Id = 3, Name = "الأثنين", Language = "Arabic", NameEn = "Monday" },
                new DayName { Id = 4, Name = "الثلاثاء", Language = "Arabic", NameEn = "Tuesday" },
                new DayName { Id = 5, Name = "الأربعاء", Language = "Arabic", NameEn = "Wednesday" },
                new DayName { Id = 6, Name = "الخميس", Language = "Arabic", NameEn = "Thursday" },
                new DayName { Id = 7, Name = "الجمعة", Language = "Arabic", NameEn = "Friday" },
                new DayName { Id = 8, Name = "لا يوجد", Language = "Arabic", NameEn = "NA" }

                );
      


        }
    }
}

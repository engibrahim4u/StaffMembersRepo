using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class EMail
    {
        [StringLength(maximumLength: 50)]
        public string Code { get; set; }
        [StringLength(maximumLength: 100)]
        public string Name { get; set; }
        public string Msg { get; set; }
        public string MsgEn { get; set; }
        [StringLength(maximumLength: 255)]
        public string Title { get; set; }
        [StringLength(maximumLength: 255)]
        public string TitleEn { get; set; }
        public bool SendEmail { get; set; }
     

    }
}

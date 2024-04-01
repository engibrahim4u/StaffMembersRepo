using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        [StringLength(maximumLength: 450)]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public string Details { get; set; }
        public DateTime TransactionTime { get; set; }

    }
}

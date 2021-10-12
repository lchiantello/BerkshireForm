using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;

namespace BerkshireForm.Models
{
    [Table("Reason")]
    class Reason
    {
        [Key]
        public int Id { get; set; }
        public string ReasonText { get; set; }
    }
}

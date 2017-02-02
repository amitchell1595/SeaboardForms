using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ContosoUniversity.Models
{
    public class DriverPayroll
    {
    

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int PayrollID2 { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string PayrollName { get; set; }

        public string TMWCode { get; set; }


    }
}
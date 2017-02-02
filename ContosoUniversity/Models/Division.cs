using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ContosoUniversity.Models
{
    public class Division
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DivisionID { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string DivisionName { get; set; }

        [StringLength(10, MinimumLength = 1)]
        public string TMWCode { get; set; }

        [StringLength(10, MinimumLength = 1)]
        public string EntityCode { get; set; }

    }
}
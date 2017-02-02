using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ContosoUniversity.Models
{
    public class Country
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CountryID { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string CountryName { get; set; }

        [StringLength(2, MinimumLength = 1)]
        public string CountryCode { get; set; }
    }
}
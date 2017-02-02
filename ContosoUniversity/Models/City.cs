using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ContosoUniversity.Models
{
    public class City
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CityID { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string CityName { get; set; }


        public int ProvinceID { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string CityFullName { get; set; }

        public string TMWCode { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ContosoUniversity.Models
{
    public class Domicile
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DomicileID { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string DomicileName { get; set; }

        [StringLength(10, MinimumLength = 1)]
        public string TMWCode { get; set; }

        [StringLength(10, MinimumLength = 1)]
        public string TMWCode2 { get; set; }
    }
}
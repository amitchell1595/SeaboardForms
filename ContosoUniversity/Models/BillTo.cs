using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ContosoUniversity.Models
{
    public class BillTo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BillToID { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string BillToName { get; set; }


        public string DropDownName
        {
            get { return TMWCode + " - " + BillToName; }
        }

        [StringLength(10, MinimumLength = 1)]
        public string TMWCode { get; set; }

        [StringLength(10, MinimumLength = 1)]
        public string DivisionCode { get; set; }
    }
}
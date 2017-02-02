using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ContosoUniversity.Models
{
    public class DPayLevel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int PayLevelID { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string PayLevelName { get; set; }


        public string TMWCode { get; set; }
    }
}
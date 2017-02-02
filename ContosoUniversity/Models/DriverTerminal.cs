using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ContosoUniversity.Models
{
    public class DriverTerminal
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TerminalID { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string TerminalName { get; set; }
  



        public string DropdownName
        {
            get { return Code2 + " - " + TerminalName; }
        }

        [StringLength(50)]
        public string Code { get; set; }

        [StringLength(50)]
        public string Code2 { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ContosoUniversity.Models
{
    public class ExitReason
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ExitReasonID { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string ExitReasonName { get; set; }


    }
}
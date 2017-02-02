using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ContosoUniversity.Models
{
    public class Entity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EntityID { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string EntityName { get; set; }

        [StringLength(10, MinimumLength = 1)]
        public string TMWCode { get; set; }
    }
}
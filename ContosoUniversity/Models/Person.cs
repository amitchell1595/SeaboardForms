using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUniversity.Models
{
    public abstract class Person
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        
        [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters.")]
        [Display(Name = "Employee Name")]
        public string Name { get; set; }

        [Display(Name = "Created By")]
        public virtual string CreatedBy { get; set; }

        [Display(Name = "Created Date")]
        public virtual DateTime CreatedDate { get; set; }

        [Display(Name = "Updated By")]
        public virtual string UpdatedBy { get; set; }

        [Display(Name = "Updated Date")]
        public virtual DateTime UpdatedDate { get; set; }

        [Display(Name = "Completed Status")]
        public bool Completed { get; set; }
    }
}
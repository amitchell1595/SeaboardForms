using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;


namespace ContosoUniversity.Models
{
    public class User : Person
    {
       
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Effective Date")]
        public DateTime EffectiveDate { get; set; }

        
        [ForeignKey("CompanyID"), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public virtual Company Company { get; set; }

        
        [ForeignKey("ChangeTypeID"), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public virtual ChangeType ChangeType { get; set; }

        
        [ForeignKey("PayrollID")]
        public virtual Payroll Payroll { get; set; }

    
        public string Role { get; set; }

        [Display(Name = "Change Type")]
        [Column("ChangeTypeID")]
        public virtual int? ChangeTypeID { get; set; }


        [Display(Name = "Company")]
        [Column("CompanyID")]
        public virtual int? CompanyID { get; set; }
        [Display(Name = "Payroll")]
        [Column("PayrollID")]
        public virtual int? PayrollID { get; set; }

        [Display(Name = "Supervisor Name")]
        public string SupervisorName { get; set; }

        [Display(Name = "Applications and Folders Required")]
        public string ApplicationsAndFolders { get; set; }

        [Display(Name = "Additional Info")]
        public string AdditionalInfo { get; set; }

        public SelectList Companies;
        public SelectList ChangeTypes;
        public SelectList Payrolls;
        public SelectList Roles;

        public string GetChangeType(){
            return ChangeType.ChangeTypeName;
        
        }
    }
}
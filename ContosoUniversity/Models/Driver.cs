using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace ContosoUniversity.Models
{

    public class Driver : Person
    {

        [ForeignKey("DriverChangeTypeID"), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public DriverChangeType DriverChangeType { get; set; }

        [Display(Name = "Driver ID")]
        public string DriverID { get; set; }

        [Display(Name = "Tractor Number")]
        public string TractorNumber { get; set; }

        [Display(Name = "Broker Unit Number")]
        public string BrokerUnitNumber { get; set; }

        [Display(Name = "Driver Type")]
        [ForeignKey("DriverTypeID"), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public virtual DriverType DriverType { get; set; }

        [Display(Name = "Driver Payroll")]
        [ForeignKey("DriverPayrollID")]
        public virtual DriverPayroll DriverPayroll { get; set; }

        [ForeignKey("CountryID")]
        public virtual Country Country { get; set; }

        [ForeignKey("ProvinceID")]
        public virtual Province Province { get; set; }

        [Display(Name = "OO Business #")]
        public string OOBusinessNumber { get; set; }

        [Display(Name = "First Name")]
        public string DriverFirstName { get; set; }

        [Display(Name = "Last Name")]
        public string DriverLastName { get; set; }

        public string Address { get; set; }

        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }
        public string SIN { get; set; }

        [Display(Name = "Home Phone")]
        public string HomePhone { get; set; }

        [Display(Name = "Cell Phone")]
        public string CellPhone { get; set; }

       
        public string Email { get; set; }

#region EmergencyInfo
        [Display(Name = "Emergency Contact")]
        public string EmergencyName { get; set; }

        [Display(Name = "Emergency Number")] 
        public string EmergencyNumber { get; set; }
#endregion

#region LicenseInfo
        [ForeignKey("LicenseProvinceID")]
        public virtual Province LicenseProvince { get; set; }

        [Display(Name = "License Class")]
        public string LicenseClass { get; set; }

        [Display(Name = "License #")]
        public string LicenseNumber { get; set; }

#endregion

#region LocationInfo
        [ForeignKey("EntityID")]
        public virtual Entity Entity { get; set; }
        [Display(Name = "Entity")]
        [Column("EntityID")]
        public virtual int? EntityID { get; set; }

        [ForeignKey("DomicileID")]
        public virtual Domicile Domicile { get; set; }
        [Display(Name = "Orbit")]
        [Column("DomicileID")]
        public virtual int? DomicileID { get; set; }

        [ForeignKey("PayLevelID")]
        public virtual DPayLevel PayLevel { get; set; }
        [Display(Name = "Pay Level")]
        [Column("PayLevelID")]
        public virtual int? PayLevelID { get; set; }
        
        [ForeignKey("DivisionID")]
        public virtual Division Division { get; set; }
        [Display(Name = "Division")]
        [Column("DivisionID")]
        public virtual int? DivisionID { get; set; }

        [Display(Name = "Driver Type")]
        [Column("DriverTypeID")]
        public virtual int? DriverTypeID { get; set; }


        [ForeignKey("TerminalID")]
        public virtual DriverTerminal Terminal { get; set; }
        [Display(Name = "Terminal")]
        [Column("TerminalID")]
        public virtual int? TerminalID { get; set; }
#endregion
      

        [Display(Name = "Country")]
        [Column("CountryID")]
        public virtual int? CountryID { get; set; }

        [Display(Name = "State/Province")]
        [Column("ProvinceID")]
        public virtual int? ProvinceID { get; set; }

        [Display(Name = "License State/Province")]
        [Column("LicenseProvinceID")]
        public virtual int? LicenseProvinceID { get; set; }

        public bool AddedToTMW { get; set; }

        [Display(Name = "Approved?")]
        public bool Approved { get; set; }

        [Display(Name = "Rejection Reason")]
        public string RejectReason { get; set; }

        [Display(Name = "Change Type")]
        [Column("DriverChangeTypeID")]
        public virtual int? DriverChangeTypeID { get; set; }


        [Display(Name = "Payroll")]
        [Column("DriverPayrollID")]
        public virtual int? DriverPayrollID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Birth Date")]
        public DateTime BirthDate { get; set; }


        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Hire Date")]
        public DateTime HireDate { get; set; }


        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Orientation Start")]
        public DateTime OrientationStart { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Orientation End")]
        public DateTime OrientationEnd { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Effective Date")]
        public DateTime DriverEffectiveDate { get; set; }


        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Last Day Worked")]
        public DateTime LastDayWorked { get; set; }

        [ForeignKey("ExitReasonID")]
        public virtual ExitReason ExitReason { get; set; }
        [Display(Name = "Exit Reason")]
        [Column("ExitReasonID")]
        public virtual int? ExitReasonID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Exit Date")]
        public DateTime ExitDate { get; set; }


        public string Comments { get; set; }


        [Display(Name = "Pending Billing/AR/AP/Payroll")]
        public bool Pending { get; set; }


        [ForeignKey("CityID")]
        public virtual City City { get; set; }
        [Display(Name = "City")]
        [Column("CityID")]
        public virtual int? CityID { get; set; }

        public SelectList Countries;
        public SelectList Provinces;
        public SelectList Divisions;
        public SelectList Orbits;
        public SelectList Cities;
        public SelectList Axles;
        public SelectList Entities;
        public SelectList BillTos;
        public SelectList Terminals;
        public SelectList SiteTypes;
      


        public SelectList Companies;
        public SelectList ChangeTypes;
        public SelectList DriverTypes;
        public SelectList DriverPayrolls;
        public SelectList ExitReasons;
        public SelectList Payrolls;
        public SelectList PayLevels;
        public SelectList Roles;

    }
}
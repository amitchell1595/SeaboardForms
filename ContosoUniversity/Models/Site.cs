using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;
using System.Web.Mvc;

namespace ContosoUniversity.Models
{
    public class Site
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RequestID { get; set; }

           public Site()
            {
                SiteUpload = new List<HttpPostedFileBase>();
            }

        public string Address { get; set; }
        public string Address2 { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        public string SiteMapLocation { get; set; }

        [Display(Name = "Alternate ID")]
        public string AltID { get; set; }

        public string Name { get; set; }


        [Display(Name = "Delivery Type")]
        public bool AutomaticDips { get; set; }


        [Display(Name = "Bill To")]
        [ForeignKey("BillToID")]
        public virtual BillTo BillTo { get; set; }
        [Display(Name = "Bill To")]
        [Column("BillToID")]
        public virtual int BillToID { get; set; }


        [Display(Name = "Site Priority")]
        public int SitePriority { get; set; }

        [DataType(DataType.ImageUrl)]
        [Display(Name = "Site Map")]
        [NotMapped]
        public List<HttpPostedFileBase> SiteUpload { get; set; }

        [Display(Name = "Comments for Driver")]
        public string Comment1 { get; set; }

        [Display(Name = "Comments for Dispatch")]
        public string Comment2 { get; set; }

        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }

   
        [RegularExpression(@"^\(?([0-9]{3})\)?[-]?([0-9]{3})[-]?([0-9]{4})$", ErrorMessage = "Please enter a properly formatted phone number.")]
        [Display(Name = "Primary Phone")]
        public string MainPhone { get; set; }

        [RegularExpression(@"^\(?([0-9]{3})\)?[-]?([0-9]{3})[-]?([0-9]{4})$", ErrorMessage = "Please enter a properly formatted phone number.")]
        [Display(Name = "Secondary Phone")]
        public string SecondaryPhone { get; set; }

        [RegularExpression(@"^\(?([0-9]{3})\)?[-]?([0-9]{3})[-]?([0-9]{4})$", ErrorMessage = "Please enter a properly formatted fax number.")]
        public string Fax { get; set; }

        [ForeignKey("CityID")]
        public virtual City City { get; set; }
        [Display(Name = "City")]
        [Column("CityID")]
        public virtual int CityID { get; set; }

        [Display(Name = "Site Type")]
        [ForeignKey("SiteTypeID")]
        public virtual SiteType SiteType { get; set; }
        [Display(Name = "Site Type")]
        [Column("SiteTypeID")]
        public virtual int SiteTypeID { get; set; }

        [ForeignKey("DivisionID")]
        public virtual Division Division { get; set; }
        [Display(Name = "Division")]
        [Column("DivisionID")]
        public virtual int DivisionID { get; set; }

        [ForeignKey("TerminalID")]
        public virtual Terminal Terminal { get; set; }
        [Display(Name = "Terminal")]
        [Column("TerminalID")]
        public virtual int TerminalID { get; set; }

        [ForeignKey("EntityID")]
        public virtual Entity Entity { get; set; }
        [Display(Name = "Entity")]
        [Column("EntityID")]
        public virtual int EntityID { get; set; }

        [ForeignKey("OrbitID")]
        public virtual Orbit Orbit { get; set; }
        [Display(Name = "Service Fleet")]
        [Column("OrbitID")]
        public virtual int OrbitID { get; set; }

        [ForeignKey("AxleID")]
        public virtual Axle Axle { get; set; }
        [Display(Name = "Axle")]
        [Column("AxleID")]
        public virtual int AxleID { get; set; }


        
        [ForeignKey("CountryID")]
        public virtual Country Country { get; set; }
        [Display(Name = "Country")]
        [Column("CountryID")]
        public virtual int CountryID { get; set; }


        [ForeignKey("ProvinceID")]
        public virtual Province Province { get; set; }
        [Display(Name = "State/Province")]
        [Column("ProvinceID")]
        public virtual int ProvinceID { get; set; }

   

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

        public bool AddedToTMW { get; set; }

        [Display(Name = "Additional Info")]
        public string AdditionalInfo{ get; set; }

        [Display(Name = "Tank Info")]
        public string TankInfo { get; set; }

        [Display(Name = "Number of Tanks")]
        public int NumberOfTanks { get; set; }

        public List<Tank> Tanks { get { return _tanks; } }
        private List<Tank> _tanks = new List<Tank>(); 

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

        public SelectList DeliveryTypes = new SelectList(
          new List<Object>{ 
                       new { value = false , text = "Order Customer"  },
                       new { value = true , text = "ASR / Dip Customer" }
                    },
          "value",
          "text",
           0);

        public SelectList SitePriorities = new SelectList(
                  new List<Object>{ 
                               new { Text = "1", Value = "1"},
                               new { Text = "2", Value = "2"},
                               new { Text = "3", Value = "3"},
                               new { Text = "4", Value = "4"},
                               new { Text = "5", Value = "5"},
                               new { Text = "6", Value = "6"}
                    },
                  "value",
                  "text",
                   0);

        public SelectList NumberOfTanksList = new SelectList(
                  new List<Object>{ 
                               new { Text = "1", Value = "1"},
                               new { Text = "2", Value = "2"},
                               new { Text = "3", Value = "3"},
                               new { Text = "4", Value = "4"},
                               new { Text = "5", Value = "5"}

                    },
                  "value",
                  "text",
                   0);




        //// Site Tanks
        //public ICollection<Tank> Tanks { get; set; }

        //// Site Requirements
        //public ICollection<LoadReq> LoadRequirements { get; set; }
    }


}
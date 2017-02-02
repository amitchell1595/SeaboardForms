using ContosoUniversity.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ContosoUniversity.Models
{
    public class Tank
    {
        private MDMSContext db = new MDMSContext();

        public Tank() {
            ProductClasses = new SelectList(db.ProductClasses, "ProductClassID", "ProductClassName");
        
        }
  
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TankID { get; set; }

        [ForeignKey("ProductClassID")]
        public virtual ProductClass ProductClass { get; set; }
        [Display(Name = "Product Class")]
        [Column("ProductClass")]
        public virtual int ProductClassID { get; set; }

        [ForeignKey("ProductCodeID")]
        public virtual ProductCode ProductCode { get; set; }
        [Display(Name = "Product Code")]
        [Column("ProductCode")]
        public virtual int ProductCodeID { get; set; }

        [ForeignKey("TankModelID")]
        public virtual TankModel TankModel { get; set; }
        [Display(Name = "Tank Model")]
        [Column("TankModel")]
        public virtual int TankModelID { get; set; }

        [ForeignKey("TankUOMID")]
        public virtual TankUOM TankUOM { get; set; }
        [Display(Name = "Tank UOM")]
        [Column("TankUOM")]
        public virtual int TankUOMID { get; set; }


        //[ForeignKey("CompanyID")]
        //public virtual Company Company { get; set; }
        //[Display(Name = "Company")]
        //[Column("CompanyID")]
        //public virtual int CompanyID { get; set; }


        public SelectList Companies;
        public SelectList ProductClasses;
        public SelectList ProductCodes;

        public int SafeFill { get; set; }
        public int ShutDown { get; set; }
        public int DailyConsumption { get; set; }
        public int Size { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string CityFullName { get; set; }
    }
}
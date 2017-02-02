using System.ComponentModel.DataAnnotations;

namespace ContosoUniversity.Models
{


    public class SiteType
    {
        public int SiteTypeID { get; set; }

        [Display(Name = "Site Type")]
        public string SiteTypeName { get; set; }

    }
}


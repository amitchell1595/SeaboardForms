using System.ComponentModel.DataAnnotations;

namespace ContosoUniversity.Models
{


    public class DriverChangeType
    {
        public int DriverChangeTypeID { get; set; }

        [Display(Name = "Change Type")]
        public string DriverChangeTypeName { get; set; }

    }
}


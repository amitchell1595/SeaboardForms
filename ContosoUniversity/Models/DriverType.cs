using System.ComponentModel.DataAnnotations;

namespace ContosoUniversity.Models
{


    public class DriverType
    {
        public int DriverTypeID { get; set; }
        public string DriverTypeName { get; set; }
        public string TMWCode { get; set; }

    }
}


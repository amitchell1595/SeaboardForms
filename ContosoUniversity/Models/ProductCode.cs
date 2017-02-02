using System.ComponentModel.DataAnnotations;

namespace ContosoUniversity.Models
{


    public class ProductCode
    {
        public int ProductCodeID { get; set; }
        public string ProductCodeName { get; set; }
        public string TMWCode { get; set; }
    }
}


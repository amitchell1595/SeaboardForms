using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUniversity.Models
{


    public class Province
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProvinceID { get; set; }
        public string ProvinceName { get; set; }
        public string ProvinceAbbr { get; set; }
        public int CountryID { get; set; }

    }
}


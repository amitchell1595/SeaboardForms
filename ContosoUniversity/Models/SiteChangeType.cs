using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUniversity.Models
{


    public class SiteChangeType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ChangeTypeID { get; set; }
        public string ChangeTypeName { get; set; }

    }
}


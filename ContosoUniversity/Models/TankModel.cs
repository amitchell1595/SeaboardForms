using System.ComponentModel.DataAnnotations;

namespace ContosoUniversity.Models
{


    public class TankModel
    {
        public int TankModelID { get; set; }
        public string TankModelName { get; set; }
        public string TankModelMake { get; set; }
        public int TankModelCapacity { get; set; }
        public string TMWCode { get; set; }
    }
}


using System.ComponentModel.DataAnnotations;

namespace ContosoUniversity.Models
{


    public class Comment
    {
        public int CommentID { get; set; }
        [Display(Name = "Unit ID")]
        public string UnitID { get; set; }
        public string Comments { get; set; }
        [Display(Name = "Ignore on Reports?")]
        public bool Ignore { get; set; }

    }
}


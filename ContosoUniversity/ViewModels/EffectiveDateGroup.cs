using System;
using System.ComponentModel.DataAnnotations;

namespace ContosoUniversity.ViewModels
{
    public class EffectiveDateGroup
    {
        [DataType(DataType.Date)]
        public DateTime? EffectiveDate { get; set; }

        public int UserCount { get; set; }
    }
}
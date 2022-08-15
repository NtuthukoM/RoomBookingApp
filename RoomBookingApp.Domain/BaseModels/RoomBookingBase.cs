using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomBookingApp.Domain.BaseModels
{
    public abstract class RoomBookingBase:IValidatableObject
    {
        [Required]
        [StringLength(100)]
        public string FullName { get; set; } = string.Empty;
        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string EmailAddress { get; set; } = String.Empty;
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(Date < DateTime.Now.Date)
            {
                yield return new ValidationResult("Date must be in the future", 
                    new[] {nameof(Date)});
            }
        }
    }
}

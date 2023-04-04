using System.ComponentModel.DataAnnotations;

namespace Factory.WebApi.Views
{
    public class EndWorkingShiftView
    {
        [Required]
        public DateTime EndShift { get; set; }
        [Required]
        public int EmployeeId { get; set; }
    }
}

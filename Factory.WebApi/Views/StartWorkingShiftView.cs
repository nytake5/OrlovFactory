using System.ComponentModel.DataAnnotations;

namespace Factory.WebApi.Views
{
    public class StartWorkingShiftView
    {
        [Required]
        public DateTime StartShift { get; set; }
        [Required]
        public int EmployeeId { get; set; }
    }
}

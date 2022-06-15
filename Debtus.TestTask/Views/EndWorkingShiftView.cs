using System.ComponentModel.DataAnnotations;

namespace Debtus.TestTask.Views
{
    public class EndWorkingShiftView
    {
        [Required]
        public DateTime EndShift { get; set; }
        [Required]
        public int EmployeeId { get; set; }
    }
}

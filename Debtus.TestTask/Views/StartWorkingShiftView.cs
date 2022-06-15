using System.ComponentModel.DataAnnotations;

namespace Debtus.TestTask.Views
{
    public class StartWorkingShiftView
    {
        [Required]
        public DateTime StartShift { get; set; }
        [Required]
        public int EmployeeId { get; set; }
    }
}

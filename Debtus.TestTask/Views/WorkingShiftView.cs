using System.ComponentModel.DataAnnotations;

namespace Debtus.TestTask.Model
{
    public class WorkingShiftView
    {
        public int ShiftId { get; set; }
        public DateTime? StartShift { get; set; }
        public DateTime? EndShift { get; set; }
        [Required]
        public int EmployeeId { get; set; }
    }
}

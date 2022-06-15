using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Debtus.TestTask.Entities
{
    public class WorkingShift
    {
        [Key]
        public int ShiftId { get; set; }
        public DateTime? StartShift { get; set; }
        public DateTime? EndShift { get; set; }

        [ForeignKey(nameof(Employee))]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}

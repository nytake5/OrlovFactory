using Debtus.TestTask.Entities;
using System.ComponentModel.DataAnnotations;

namespace Debtus.TestTask.Model
{
    public class EmployeeView
    {
        public int PassNumber { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Patronymic { get; set; }
        [Required]
        public Post Post { get; set; }
    }
}

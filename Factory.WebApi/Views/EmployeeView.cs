using System.ComponentModel.DataAnnotations;
using Entities;

namespace Factory.WebApi.Views
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

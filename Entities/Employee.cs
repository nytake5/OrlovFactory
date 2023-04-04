using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities;

public class Employee
{
    [Key] 
    public int PassNumber { get; set; }
    
    [Required] 
    public string FirstName { get; set; }
    
    [Required] 
    public string LastName { get; set; }
    
    [Required] 
    public string Patronymic { get; set; }
    
    [Required] 
    public Post Post { get; set; }

    [NotMapped] 
    public int? NumberOfFines { get; set; }
    public IEnumerable<WorkingShift>? WorkingShifts { get; set; }
}
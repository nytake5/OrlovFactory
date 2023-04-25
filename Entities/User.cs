using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Entities;

public class User
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Login { get; set; }
    
    [Required]
    public string Password { get; set; }

    public long ChatId { get; set; }
    public Guid? Token { get; set; }

    [ForeignKey(nameof(Employee))] 
    public int EmployeeId { get; set; }
    
    [NotMapped]
    [JsonIgnore]
    public Employee? EmployeeFk { get; set; }
}
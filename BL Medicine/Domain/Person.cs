using System.ComponentModel.DataAnnotations;

namespace BL_Medicine.Domain;

public class Person
{
    [Required]
    public int Id { get; set; }
    [Required]
    public string? Firstname { get; set; }
    [Required]
    public string? Surname { get; set; }
    [Required]
    public string? Email { get; set; }

    public DateTime? DayOfBirth { get; set; }
}
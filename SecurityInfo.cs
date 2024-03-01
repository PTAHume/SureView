using System.ComponentModel.DataAnnotations;

namespace ViewModels;

public class SecurityInfo
{
    [Required(ErrorMessage ="Please enter an input.")]
    public string UserInput { get; set; } = string.Empty;

    public int ServerNumber { get; set; }

    public int AlarmId { get; set; }
}

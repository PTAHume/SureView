using System.ComponentModel.DataAnnotations;

namespace ViewModels;

public class SecurityInfo
{
    [Required]
    public string UserInput { get; set; } = string.Empty;

    public int ServerNumber { get; set; }

    public int AlarmId { get; set; }
}

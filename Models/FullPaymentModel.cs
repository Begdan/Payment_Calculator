using System.ComponentModel.DataAnnotations;

namespace Payment_Calculator.Models;

public class FullPaymentModel
{
    [Required]
    public double BaseDebt { get; set; } // if BaseDebt == 0 => all fields are 0
    [Required]
    public double Interest { get; set; }
    [Required]
    public double Penalty { get; set; } // if Penalty != 0 => BaseDebt != 0, interest != 0
    public double Total => BaseDebt + Interest + Penalty;
}
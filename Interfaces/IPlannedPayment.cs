namespace Payment_Calculator.Interfaces;

public interface IPlannedPayment
{
    DateTime PaymentDate { get; }
    double BaseDebt { get; }
    double Interest { get; }
    double RemainingBaseDebt { get; }
}
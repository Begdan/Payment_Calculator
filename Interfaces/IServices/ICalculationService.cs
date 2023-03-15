using Payment_Calculator.Enums;

namespace Payment_Calculator.Interfaces.IServices;

public interface ICalculationService
{
    public double GetAmountFromAccount(ILoan loan, AccountBaseType accountBaseType, DateTime currentDate);
    public double GetAmountFromOperations(ILoan loan, AccountType type, DateTime currentDate);
}
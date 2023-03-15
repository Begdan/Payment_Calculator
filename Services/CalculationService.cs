using Payment_Calculator.Enums;
using Payment_Calculator.Interfaces;
using Payment_Calculator.Interfaces.IServices;

namespace Payment_Calculator.Services;

public class CalculationService : ICalculationService
{
    public double GetAmountFromAccount(ILoan loan, AccountBaseType accountBaseType, DateTime currentDate)
    {
        var overdueDebt = GetAmountFromOperations(loan,
            accountBaseType == AccountBaseType.Base
                ? AccountType.OVERDUE_BASE_DEBT
                : AccountType.OVERDUE_INTEREST,
            currentDate);
        var prepaidDebt = GetAmountFromOperations(loan,
            accountBaseType == AccountBaseType.Base
                ? AccountType.PREPAID_BASE_DEBT
                : AccountType.PREPAID_INTEREST,
            currentDate);
        var baseDebt = GetAmountFromOperations(loan, 
            accountBaseType == AccountBaseType.Base
                ? AccountType.BASE_DEBT
                : AccountType.INTEREST,
            currentDate);

        return baseDebt + overdueDebt - prepaidDebt;
    }
    public double GetAmountFromOperations(ILoan loan, AccountType type, DateTime currentDate) => 
        loan
            .GetOperations()
            .Where(x => x.Date <= currentDate && x.AccountType == type)
            .Select(x => x.Amount)
            .Sum();
}
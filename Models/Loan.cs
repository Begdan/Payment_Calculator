using Payment_Calculator.Enums;
using Payment_Calculator.Interfaces;

namespace Payment_Calculator.Models;

public class Loan : ILoan
{
    public double Amount { get; }
    public double InterestRate { get; }
    public DateTime StartDate { get; } // date when client took a money
    public DateTime EndDate { get; } // assuming date of last payment (by payments schedule)
    public DateTime CloseDate { get; } // real date of last payment (!= default only for closed loans)
    public LoanStatus Status { get; }

    public List<IPlannedPayment> GetPaymentSchedule()
    {
        throw new NotImplementedException();
    }

    public List<IOperation> GetOperations()
    {
        throw new NotImplementedException();
    }
    
}
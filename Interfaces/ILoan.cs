using Payment_Calculator.Enums;

namespace Payment_Calculator.Interfaces;

public interface ILoan
{
    double Amount { get; }
    double InterestRate { get; }
    DateTime StartDate { get; } // date when client took a money
    DateTime EndDate { get; } // assuming date of last payment (by payments schedule)
    DateTime CloseDate { get; } // real date of last payment (!= default only for closed loans)
    LoanStatus Status { get; }
    List<IPlannedPayment> GetPaymentSchedule();
    List<IOperation> GetOperations();
}
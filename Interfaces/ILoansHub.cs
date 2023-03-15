namespace Payment_Calculator.Interfaces;

public interface ILoansHub
{
    ILoan GetLoanById(int loanId); //returns null if object was not found
}
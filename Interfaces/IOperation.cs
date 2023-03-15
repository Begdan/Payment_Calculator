using Payment_Calculator.Enums;

namespace Payment_Calculator.Interfaces;

public interface IOperation
{
    int Id { get; }
    double Amount { get; } // can be only positive or negative
    DateTime Date { get; }
    AccountType AccountType { get; }
}
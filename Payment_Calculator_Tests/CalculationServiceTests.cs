using Microsoft.OpenApi.Models;
using Moq;
using NUnit.Framework;
using Payment_Calculator.Enums;
using Payment_Calculator.Interfaces;
using Payment_Calculator.Interfaces.IServices;
using Payment_Calculator.Models;
using Payment_Calculator.Services;

namespace Payment_Calculator_Tests;

public class CalculationServiceTests
{
    private Mock<ILoansHub> _mockLoansHub;
    private Mock<ICalculationService> _mockCalculationService;

    [SetUp]
    public void Setup()
    {
        _mockLoansHub = new Mock<ILoansHub>();
        _mockCalculationService = new Mock<ICalculationService>();
         
        var loanMock = new Mock<ILoan>();
        loanMock.SetupGet(x => x.Amount).Returns(100000);
        loanMock.SetupGet(x => x.InterestRate).Returns(0.1);
        loanMock.SetupGet(x => x.StartDate).Returns(new DateTime(2023, 1, 1));
        loanMock.SetupGet(x => x.EndDate).Returns(new DateTime(2023, 6, 1));
        loanMock.SetupGet(x => x.Status).Returns(LoanStatus.NORMAL);
        var operationMock = new Mock<IOperation>();
        operationMock.SetupGet(x => x.Amount).Returns(100000);
        operationMock.SetupGet(x => x.Date).Returns(new DateTime(2023, 1, 1));
        operationMock.SetupGet(x => x.AccountType).Returns(AccountType.BASE_DEBT);
        var operations = new List<IOperation>()
        {
            operationMock.Object
        };
        loanMock.Setup(x => x.GetOperations()).Returns(operations);

        _mockLoansHub.Setup(x => x.GetLoanById(It.IsAny<int>())).Returns(loanMock.Object);
        _mockCalculationService.Setup(x => x.GetAmountFromOperations(It.IsAny<ILoan>(), AccountType.BASE_DEBT, It.IsAny<DateTime>())).Returns(1000);
    }

    [Test]
    public void GetAmountFromAccountTest()
    {
        //arrange
        var loan = _mockLoansHub.Object.GetLoanById(1);
        var calculationService = new CalculationService();

        //act
        var result = calculationService.GetAmountFromAccount(loan, AccountBaseType.Base, new DateTime(2023, 3, 1));

        //assert
        Assert.AreEqual(100000, result);
    }
    
    [Test]
    public void GetAmountFromOperationsTest()
    {
        //arrange
        var loan = _mockLoansHub.Object.GetLoanById(1);
        var calculationService = new CalculationService();

        //act
        var result = calculationService.GetAmountFromOperations(loan, AccountType.BASE_DEBT, new DateTime(2023, 3, 1));

        //assert
        Assert.AreEqual(100000, result);
    }
}
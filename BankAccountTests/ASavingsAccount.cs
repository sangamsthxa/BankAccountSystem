using System;
using BankAccountLibrary;

namespace BankAccountTests;

public class ASavingsAccount
{

[Test]
  public void ShouldSetBalanceAnnualInterestrateAndStatusActiveWhenConstructed(){
        //Arrange
        decimal initialBalance = 100m;
        decimal annualInterestRate = 0.05m;

        //Act
        var sut = new SavingsAccount(initialBalance, annualInterestRate);
        Assert.That(sut.Balance, Is.EqualTo(initialBalance));
        Assert.That(sut.AnnualInterestRate, Is.EqualTo(annualInterestRate));
        Assert.That(sut.Status, Is.EqualTo(AccountStatus.Active));
    }

[Test]
  public void ShouldSetBalanceAnnualInterestrateAndStatusInActiveWhenConstructed(){
        //Arrange
        decimal initialBalance = 20m;
        decimal annualInterestRate = 0.05m;

        //Act
        var sut = new SavingsAccount(initialBalance, annualInterestRate);
        Assert.That(sut.Balance, Is.EqualTo(initialBalance));
        Assert.That(sut.AnnualInterestRate, Is.EqualTo(annualInterestRate));
        Assert.That(sut.Status, Is.EqualTo(AccountStatus.Inactive));
    }

    [Test]
    public void ShouldIncreaseBalanceAfterDeposit(){
        //Arrange
        decimal initialBalance = 100m;
        decimal annualInterestRate = 0.05m;
        var sut = new SavingsAccount(initialBalance,annualInterestRate);

        decimal depositAmount = 100m;
        //Act
        sut.Deposit(depositAmount);

        //Assert
        Assert.That(sut.Balance, Is.EqualTo(200m));
        Assert.That(sut.Status, Is.EqualTo(AccountStatus.Active));
    }

[Test]
    public void ShouldChangeStatusToActiveAfterDeposit(){
        //Arrange
        decimal initialBalance = 20m;
        decimal annualInterestRate = 0.05m;
        //Act
        var sut = new SavingsAccount(initialBalance,annualInterestRate);

        //Assert
        Assert.That(sut.Status, Is.EqualTo(AccountStatus.Inactive));

        //Arrange
        decimal depositAmount = 100m;

        //Act
        sut.Deposit(depositAmount);

        //Assert
        Assert.That(sut.Balance, Is.EqualTo(120m));
        Assert.That(sut.AnnualInterestRate, Is.EqualTo(annualInterestRate));
        Assert.That(sut.Status, Is.EqualTo(AccountStatus.Active));
    }

[Test]
    public void ShouldNotChangeStatusToActiveAfterDeposit(){
        //Arrange
        decimal initialBalance = 15m;
        decimal annualInterestRate = 0.05m;
        //Act
        var sut = new SavingsAccount(initialBalance,annualInterestRate);

        //Assert
        Assert.That(sut.Status, Is.EqualTo(AccountStatus.Inactive));

        //Arrange
        decimal depositAmount = 5m;

        //Act
        sut.Deposit(depositAmount);

        //Assert
        Assert.That(sut.Balance, Is.EqualTo(20m));
        Assert.That(sut.AnnualInterestRate, Is.EqualTo(annualInterestRate));
        Assert.That(sut.Status, Is.EqualTo(AccountStatus.Inactive));
    }

[Test]
    public void ShouldDecreaseBalanceAfterWithdrawWhenActive(){
        //Arrange
        decimal initialBalance = 100m;
        decimal annualInterestRate = 0.05m;
        //Act
        var sut = new SavingsAccount(initialBalance,annualInterestRate);
        //Assert
        Assert.That(sut.Status, Is.EqualTo(AccountStatus.Active));

        //Arrange
        decimal withrawAmount = 10m;
        //Act
        sut.Withdraw(withrawAmount);

        //Assert
        Assert.That(sut.Balance, Is.EqualTo(90m));
        Assert.That(sut.Status, Is.EqualTo(AccountStatus.Active));
    }

[Test]
    public void ShouldNotChangeBalanceAfterWithdrawalWhenInactive()
    {
        //Arrange
        decimal initialBalance = 20m;
        decimal annualInterestRate = 0.05m;
        //Act
        var sut = new SavingsAccount(initialBalance,annualInterestRate);
        
        //Assert
        Assert.That(sut.Status, Is.EqualTo(AccountStatus.Inactive));

        //Act
        sut.Withdraw(10m);

        //Assert
        Assert.That(sut.Balance, Is.EqualTo(20m));
        Assert.That(sut.Status, Is.EqualTo(AccountStatus.Inactive));

    }
[Test]
    public void ShouldChangeStatusAfterWithdraw(){
        //Arrange
        decimal initialBalance = 30m;
        decimal annualInterestRate = 0.05m;
        //Act
        var sut = new SavingsAccount(initialBalance,annualInterestRate);
        //Assert
        Assert.That(sut.Status, Is.EqualTo(AccountStatus.Active));

        //Arrange
        decimal withrawAmount = 10m;
        //Act
        sut.Withdraw(withrawAmount);

        //Assert
        Assert.That(sut.Balance, Is.EqualTo(20m));
        Assert.That(sut.Status, Is.EqualTo(AccountStatus.Inactive));
    }

 [Test]

    public void ShouldRunMonthlyProcess(){
       //Arrange
        decimal initialBalance = 80m;
        decimal annualInterestRate = 0.05m;
        var sut = new SavingsAccount(initialBalance,annualInterestRate);
         //Act
         sut.Deposit(10m);

         sut.Withdraw(20m);

        //Assert
        Assert.That(sut.NumberOfDeposits, Is.EqualTo(1));
        Assert.That(sut.NumberOfWithdrawals, Is.EqualTo(1));
        Assert.That(sut.Balance, Is.EqualTo(70m));
        Assert.That(sut.Status,Is.EqualTo(AccountStatus.Active));
       
       //Act
        sut.MonthlyProcess();

        //Assert
        Assert.That(sut.Balance, Is.EqualTo(67.27m).Within(0.25));
        Assert.That(sut.NumberOfDeposits, Is.EqualTo(0));
        Assert.That(sut.NumberOfWithdrawals, Is.EqualTo(0));
        Assert.That(sut.MonthlyServiceCharge, Is.EqualTo(0));

    }

    [Test]

    public void ShouldRunMonthlyProcessWithMultipleTransactions(){
       //Arrange
        decimal initialBalance = 100m;
        decimal annualInterestRate = 0.05m;
        var sut = new SavingsAccount(initialBalance,annualInterestRate);
         //Act
         sut.Deposit(10m);
         sut.Deposit(20m);
         sut.Deposit(10m);
         sut.Deposit(9m);

         sut.Withdraw(20m);
         sut.Withdraw(10m);
         sut.Withdraw(8m);
         sut.Withdraw(4m);
         sut.Withdraw(5m);

        //Assert
        Assert.That(sut.NumberOfDeposits, Is.EqualTo(4));
        Assert.That(sut.NumberOfWithdrawals, Is.EqualTo(5));
        Assert.That(sut.Balance,Is.EqualTo(102));
       
       //Act
        sut.MonthlyProcess();

        //Assert
        Assert.That(sut.Balance, Is.EqualTo(98.4).Within(0.25));
        Assert.That(sut.NumberOfDeposits, Is.EqualTo(0));
        Assert.That(sut.NumberOfWithdrawals, Is.EqualTo(0));
        Assert.That(sut.MonthlyServiceCharge, Is.EqualTo(0));

    }





}

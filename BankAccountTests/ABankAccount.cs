using BankAccountLibrary;

namespace BankAccountTests;

public class ABankAccount
{

[Test]
    public void ShouldSetBalanceAndAnnualInterestrateWhenConstructed(){
        //Arrange
        decimal initialBalance = 100m;
        decimal annualInterestRate = 0.05m;

        //Act
        var sut = new BankAccount(initialBalance, annualInterestRate);
        Assert.That(sut.Balance, Is.EqualTo(initialBalance));
        Assert.That(sut.AnnualInterestRate, Is.EqualTo(annualInterestRate));
    }

[Test]
    public void ShouldIncreaseBalanceAfterDeposit(){
        //Arrange
        decimal initialBalance = 100m;
        decimal annualInterestRate = 0.05m;
        var sut = new BankAccount(initialBalance,annualInterestRate);

        decimal depositAmount = 100m;
        //Act
        sut.Deposit(depositAmount);

        //Assert
        Assert.That(sut.Balance, Is.EqualTo(200m));
        Assert.That(sut.NumberOfDeposits,Is.EqualTo(1));
    }

[Test]
    public void ShouldDecreaseBalanceAfterWithdraw(){
        //Arrange
        decimal initialBalance = 100m;
        decimal annualInterestRate = 0.05m;
        var sut = new BankAccount(initialBalance,annualInterestRate);

        decimal withrawAmount = 10m;
        //Act
        sut.Withdraw(withrawAmount);

        //Assert
        Assert.That(sut.Balance, Is.EqualTo(90m));
        Assert.That(sut.NumberOfWithdrawals,Is.EqualTo(1));
    }

  [Test]
    public void ShouldCalculateInterest(){
        //Arrange
        decimal initialBalance = 100m;
        decimal annualInterestRate = 0.05m;
        var sut = new BankAccount(initialBalance,annualInterestRate);
        
        //Act
        sut.CalculateInterest();

        //Assert
        Assert.That(sut.Balance, Is.EqualTo(100.41m).Within(0.25));
    } 

    [Test]
    public void ShouldCalculateInterestWithVariousBalanceAndRate(){
        //Arrange
        var sut1 = new BankAccount(50m,0.05m);
        var sut2 = new BankAccount(75m,0.15m);
        var sut3 = new BankAccount(30m,0.25m);
        var sut4 = new BankAccount(90m,0.35m);
        //Act
        sut1.CalculateInterest();
        sut2.CalculateInterest();
        sut3.CalculateInterest();
        sut4.CalculateInterest();

        //Assert
        Assert.That(sut1.Balance, Is.EqualTo(50.2m).Within(0.25));
        Assert.That(sut2.Balance, Is.EqualTo(75.93m).Within(0.25));
        Assert.That(sut3.Balance, Is.EqualTo(30.62m).Within(0.25));
        Assert.That(sut4.Balance, Is.EqualTo(92.62m).Within(0.25));
    } 

    [Test]

    public void ShouldRunMonthlyProcess(){
       //Arrange
        decimal initialBalance = 100m;
        decimal annualInterestRate = 0.05m;
        var sut = new BankAccount(initialBalance,annualInterestRate);
         //Act
         sut.Deposit(10m);

         sut.Withdraw(20m);

        //Assert
        Assert.That(sut.NumberOfDeposits, Is.EqualTo(1));
        Assert.That(sut.NumberOfWithdrawals, Is.EqualTo(1));
       
       //Act
        sut.MonthlyProcess();

        //Assert
        Assert.That(sut.Balance, Is.EqualTo(87.36m).Within(0.25));
        Assert.That(sut.NumberOfDeposits, Is.EqualTo(0));
        Assert.That(sut.NumberOfWithdrawals, Is.EqualTo(0));
        Assert.That(sut.MonthlyServiceCharge, Is.EqualTo(0));

    }

     [Test]

    public void ShouldRunMonthlyProcessWithMultipleTransactions(){
       //Arrange
        decimal initialBalance = 100m;
        decimal annualInterestRate = 0.05m;
        var sut = new BankAccount(initialBalance,annualInterestRate);
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
        Assert.That(sut.Balance, Is.EqualTo(99.41m).Within(0.25));
        Assert.That(sut.NumberOfDeposits, Is.EqualTo(0));
        Assert.That(sut.NumberOfWithdrawals, Is.EqualTo(0));
        Assert.That(sut.MonthlyServiceCharge, Is.EqualTo(0));

    }

    [Test]

    public void ShouldNotDepositAmountBelowZero(){
         //Arrange
        decimal initialBalance = 100m;
        decimal annualInterestRate = 0.05m;
        var sut = new BankAccount(initialBalance,annualInterestRate);
        decimal depositAmount = -6m;
        //Act
        sut.Deposit(depositAmount);

        //Assert
        Assert.That(sut.Balance, Is.EqualTo(100m));

        //Arrange
        depositAmount =0;
        //Act
        sut.Deposit(depositAmount);

        //Assert
        Assert.That(sut.Balance, Is.EqualTo(100m));
    }

[Test]
    public void ShouldNotWithdrawAmountBelowZero(){
        //Arrange
        decimal initialBalance = 100m;
        decimal annualInterestRate = 0.05m;
        var sut = new BankAccount(initialBalance,annualInterestRate);

        decimal withrawAmount = -5m;
        //Act
        sut.Withdraw(withrawAmount);

        //Assert
        Assert.That(sut.Balance, Is.EqualTo(100m));

        //Arrange
        withrawAmount = 0;
        //Act
        sut.Withdraw(withrawAmount);

        //Assert
        Assert.That(sut.Balance, Is.EqualTo(100m));
    }



}

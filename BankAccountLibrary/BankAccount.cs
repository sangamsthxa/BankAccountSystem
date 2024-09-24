using System;

namespace BankAccountLibrary;

public class BankAccount
{
    public BankAccount(decimal initialBalance, decimal annualInterestRate)
    {
        Balance = initialBalance;
        AnnualInterestRate = annualInterestRate;
    }

    public decimal Balance { get; private set; }
    public decimal AnnualInterestRate { get; }

    public decimal MonthlyServiceCharge = 3m;

    public int NumberOfDeposits { get; set; }

    public int NumberOfWithdrawals { get; set; }

    public void CalculateInterest()
    {
        decimal MonthlyInterestRate = AnnualInterestRate/12;
        decimal MonthlyInterest = Balance * MonthlyInterestRate;
        Balance = Balance + MonthlyInterest;
        

    }

    public virtual void Deposit(decimal depositAmount){
        if(depositAmount > 0){
            Balance +=depositAmount;
            NumberOfDeposits +=1;
        }
        else{
            Console.WriteLine("Invalid Deposit");
        }
    }

    public virtual void Withdraw(decimal withrawAmount){
        if(withrawAmount > 0){
        Balance -=withrawAmount;
        NumberOfWithdrawals +=1;
        }
        else{
            Console.WriteLine("Invalid Withdraw");
        }
        
    }

    public virtual void MonthlyProcess()
    {
    Console.WriteLine("Check"+ MonthlyServiceCharge);
       Balance = Balance - MonthlyServiceCharge;
       CalculateInterest();
       NumberOfDeposits =0;
       NumberOfWithdrawals=0;
       MonthlyServiceCharge=0;
    }
}

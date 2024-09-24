using System;
using System.Runtime.CompilerServices;

namespace BankAccountLibrary;

public class SavingsAccount:BankAccount
{
    public SavingsAccount(decimal initialBalance, decimal annualInterestRate) : base(initialBalance, annualInterestRate)
    {
      
        if(initialBalance > 25){
              Status=AccountStatus.Active;
        }
        else{
            Status=AccountStatus.Inactive;
        }
    }

   public AccountStatus Status { get; set; }

     public override void Withdraw(decimal withrawAmount){
        if (Status.Equals(AccountStatus.Active)){
            base.Withdraw(withrawAmount);
        }
        if(Balance <=25){
            Status= AccountStatus.Inactive;
        }   
    }

    public override void Deposit(decimal depositAmount){
        base.Deposit(depositAmount);
        if(Status.Equals(AccountStatus.Inactive) && Balance <= 25){
            Status =AccountStatus.Inactive;
        }
        else{
            Status =AccountStatus.Active;
        }
    }

    public override void MonthlyProcess(){
        if(NumberOfWithdrawals > 4){
            MonthlyServiceCharge += NumberOfWithdrawals - 4;
        }
        base.MonthlyProcess();
         if(Balance <= 25){
            Status =AccountStatus.Inactive;
        }
        else{
            Status =AccountStatus.Active;
        }
            
    }
    

}

  public enum AccountStatus{
    Inactive ,Active
   }

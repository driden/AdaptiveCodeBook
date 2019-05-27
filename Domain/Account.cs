using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Account
    {
        public decimal Balance { get; private set; }

        public virtual void AddTransaction(decimal amount)
        {
            Balance += amount;
        }
    }
}

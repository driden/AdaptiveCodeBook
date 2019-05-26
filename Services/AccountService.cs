namespace Services
{
    using RepositoryInterfaces;
    using System;

    public class AccountService
    {
        private readonly IAccountRepository repository;

        public AccountService(IAccountRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException(nameof(repository), "A valid account repository must be supplied.");
            }

            this.repository = repository;
        }

        public void AddTransactionToAccount(string uniqueAccountName, decimal amount)
        {
            var account = repository.GetByName(uniqueAccountName);
            account.AddTransaction(amount);
        }
    }
}

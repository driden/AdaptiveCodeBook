namespace Services
{
    using Domain;
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
            if (account == null) return;

            try
            {
                account.AddTransaction(amount);
            }
            catch (DomainException  domainException)
            {
                throw new ServiceException("Adding transaction to account failed.", domainException);
            }
        }
    }
}

using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RepositoryInterfaces;
using Services;
using System;

namespace ServiceTests
{
    [TestClass]
    public class AccountServiceTests
    {
        [TestInitialize]
        public void SetUp()
        {
            mockAccount = new Mock<Account>();
            mockRepository = new Mock<IAccountRepository>();
            sut = new AccountService(mockRepository.Object);
        }

        private const string TRADING_ACCOUNT = "Trading account";
        private Mock<Account> mockAccount;
        private Mock<IAccountRepository> mockRepository;
        private AccountService sut;

        [TestMethod]
        public void AddingTransactionChangesBalance()
        {
            // Arrange
            var account = new Account();

            // Act
            account.AddTransaction(200m);

            // Assert
            Assert.AreEqual(200m, account.Balance);
        }

        [TestMethod]
        public void AccountsHaveAnOpeningBalanceOfZero()
        {
            // Arrange

            // Act
            var account = new Account();

            // Assert
            Assert.AreEqual(0m, account.Balance);
        }

        [TestMethod]
        public void Adding100TransactionChangesBalance()
        {
            // Arrange
            var account = new Account();

            // Act
            account.AddTransaction(100m);

            // Assert

            Assert.AreEqual(100m, account.Balance);
        }

        [TestMethod]
        public void AddingTransactionToAccountDelegatesToAccountInstance()
        {
            // Arrange
            var account = new Account();
            mockRepository.Setup(repo => repo.GetByName(TRADING_ACCOUNT)).Returns(account);

            // Act
            sut.AddTransactionToAccount(TRADING_ACCOUNT, 200m);

            // Assert
            Assert.AreEqual(200m, account.Balance);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CannotCreateAccountServiceWithNullAccountRepository()
        {
            // Arrange

            // Act
            new AccountService(null);

            // Assert
        }

        [TestMethod]
        public void DoNotThrowWhenAccountIsNotFound()
        {
            // Arrange

            // Act
            sut.AddTransactionToAccount(TRADING_ACCOUNT, 100m);

            // Asserts
        }

        [TestMethod]
        public void AccountExceptionsAreWrappedInThrownServiceException()
        {
            // Arrange
            mockAccount.Setup(a => a.AddTransaction(100m)).Throws<DomainException>();
            mockRepository.Setup(r => r.GetByName("Trading Account")).Returns(mockAccount.Object);

            // Act
            try
            {
                sut.AddTransactionToAccount("Trading Account", 100m);
            }
            catch (ServiceException serviceException)
            {
                // Assert
                Assert.IsInstanceOfType(serviceException.InnerException, typeof(DomainException));
            }
        }
    }
}

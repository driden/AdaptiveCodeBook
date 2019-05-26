using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain;
using Services;
using Moq;
using RepositoryInterfaces;
using System;

namespace ServiceTests
{
    [TestClass]
    public class AccountServiceTests
    {
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
            var mockRepository = new Mock<IAccountRepository>();
            mockRepository.Setup(repo => repo.GetByName("Trading Account")).Returns(account);
            var sut = new AccountService(mockRepository.Object);

            // Act
            sut.AddTransactionToAccount("Trading Account", 200m);

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
    }
}

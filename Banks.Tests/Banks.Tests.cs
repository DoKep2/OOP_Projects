using System;
using Banks.Accounts;
using Banks.Classes;
using Banks.Repositories;
using Banks.Service;
using NUnit.Framework;

namespace Banks.Tests
{
    [TestFixture]
    public class BanksTests
    {
        private BankRepository _bankRepository;
        private AccountRepository _accountRepository;
        private ClientRepository _clientRepository;
        private Client _nonApprovedClient;
        private Client _approvedClient;
        private AccountFactory _accountFactory;
        [SetUp]
        public void Setup()
        {
            _bankRepository = new BankRepository();
            _accountRepository = new AccountRepository();
            _clientRepository = new ClientRepository();
            _accountFactory = new AccountFactory();
            _nonApprovedClient = new Client.ClientBuilder().WithName("Roman").WithSurname("Makarevich").Build();
            _approvedClient = new Client.ClientBuilder()
                .WithName("Roman")
                .WithSurname("Makarevich")
                .WithAddress("Sweet home")
                .WithPassportId("777")
                .Build();
        }

        [Test]
        public void CreateClientWithoutRequiredField_ThrowException()
        {
            Assert.Catch<Exception>(() =>
            {
                Client client = new Client.ClientBuilder().WithName("Roman").WithPassportId("777").Build();
            });
        }

        [Test]
        public void AddNonRequiredFieldsToNonApprovedClient_ClientBecomeApproved()
        {
            Client client = new Client.ClientBuilder().WithName("Roman").WithSurname("Makarevich").Build();
            client = client.ToBuilder().WithAddress("Sweet home").WithPassportId("777").Build();
            Assert.IsTrue(client.IsApproved);
        }

        [Test]
        public void CreateAccount_AccountAppearsInRepo()
        {
            var bank = new Bank("Bank1", new Conditions(1, 2, 3),
                    new ExtraConditions(5));
            Account account = _accountFactory.CreateAccount(bank, _nonApprovedClient);
            CollectionAssert.Contains(bank.AccountRepository.GetAll(), account);
        }
    }
}
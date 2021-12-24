using System;
using System.Collections.Generic;
using Banks.Accounts;
using Banks.Chain.Handlers;
using Banks.Chain.Requests;
using Banks.Classes;
using Banks.Service;

namespace Banks.UI
{
    internal static class Program
    {
        private static void Main()
        {
            var topUpHandler = new TopUpHandler();
            var withDrawHandler = new WithDrawHandler();
            var transferHandler = new TransferHandler();
            var validationHandler = new ValidationHandler();
            var extraValidationHandler = new ExtraValidationHandler();
            var confirmHandler = new ConfirmHandler();
            extraValidationHandler.SetNext(validationHandler)
                .SetNext(confirmHandler)
                .SetNext(topUpHandler)
                .SetNext(withDrawHandler)
                .SetNext(transferHandler);
            var accountFactory = new AccountFactory();
            CentralBank.BankRepository.RegisterBank(
                new Bank(
                    "Bank1",
                    new Conditions(1, 2, 3),
                    new ExtraConditions(4)));
            Console.WriteLine("1. Sign up");
            while (Console.ReadLine() != "1")
            {
                Console.WriteLine("Enter 1 to sign up");
            }

            Console.WriteLine("Enter your name");
            string name;
            while ((name = Console.ReadLine()) == string.Empty)
            {
                Console.WriteLine("Invalid name");
            }

            Console.WriteLine("Enter your surname");
            string surname;
            while ((surname = Console.ReadLine()) == string.Empty)
            {
                Console.WriteLine("Invalid surname");
            }

            Console.WriteLine("Enter your address(optional)");
            string address = Console.ReadLine();
            Console.WriteLine("Enter your passportID(optional)");
            string passportId = Console.ReadLine();
            Client client = new Client.ClientBuilder()
                .WithName(name)
                .WithSurname(surname)
                .WithAddress(address)
                .WithPassportId(passportId)
                .Build();
            CentralBank.ClientRepository.RegisterClient(client);
            Console.WriteLine("Sign up successfully\n Choose option:");
            var options = new List<string>()
            {
                "0. Exit",
                "1. Create account",
                "2. Look account data",
                "3. Do transaction",
                "4. Print accounts list",
            };
            while (true)
            {
                foreach (string option in options)
                {
                    Console.WriteLine(option);
                }

                int optionNumber;
                while ((optionNumber = Convert.ToInt32(Console.ReadLine())) > options.Count)
                {
                    Console.WriteLine($"Enter number from 0 to {options.Count}");
                }

                switch (optionNumber)
                {
                    case 0:
                        return;
                    case 1:
                    {
                        Console.WriteLine("Enter account type:");
                        var accountsTypes = new List<string>()
                        {
                            "Credit",
                            "Deposit",
                            "Debit",
                        };
                        foreach (string type in accountsTypes)
                        {
                            Console.WriteLine(type);
                        }

                        string accountType;
                        while (!accountsTypes.Contains(accountType = Console.ReadLine()))
                        {
                            Console.WriteLine("Invalid type");
                        }

                        Console.WriteLine("Enter bank name");
                        string bankName = Console.ReadLine();
                        Bank bank = CentralBank.BankRepository.GetBank(bankName);
                        Account newAccount = null;
                        switch (accountType)
                        {
                            case "Credit":
                                newAccount =
                                    accountFactory.CreateAccount(bank, client, bank.Conditions.CreditBalanceLimit);
                                break;
                            case "Deposit":
                                newAccount = accountFactory.CreateAccount(bank, client, DateTime.Now.AddYears(4));
                                break;
                            case "Debit":
                                newAccount = accountFactory.CreateAccount(bank, client);
                                break;
                        }

                        break;
                    }

                    case 2:
                    {
                        Console.WriteLine(client);
                        break;
                    }

                    case 3:
                    {
                        Console.WriteLine("Enter transaction");
                        var transactions = new List<string>()
                        {
                            "TopUp",
                            "WithDraw",
                            "Transfer",
                        };
                        foreach (string transaction in transactions)
                        {
                            Console.WriteLine(transaction);
                        }

                        string currentTransaction = Console.ReadLine();
                        switch (currentTransaction)
                        {
                            case "TopUp":
                            {
                                Console.WriteLine("Bank name");
                                string bankName = Console.ReadLine();
                                Bank bank = CentralBank.BankRepository.GetBank(bankName);
                                Console.WriteLine("Enter account id");
                                int id = Convert.ToInt32(Console.ReadLine());
                                Account account = bank.AccountRepository.GetAccount(id);
                                Console.WriteLine("Enter cash to top up");
                                int cashToTopUp = Convert.ToInt32(Console.ReadLine());
                                Request request = new TopUpRequest(
                                    bank,
                                    Convert.ToInt32(client.PassportId),
                                    id,
                                    cashToTopUp);
                                if (client.IsApproved)
                                {
                                    validationHandler.Handle(request);
                                }
                                else
                                {
                                    extraValidationHandler.Handle(request);
                                }

                                break;
                            }

                            case "WithDraw":
                            {
                                Console.WriteLine("Bank name");
                                string bankName = Console.ReadLine();
                                Bank bank = CentralBank.BankRepository.GetBank(bankName);
                                Console.WriteLine("Enter account id");
                                int id = Convert.ToInt32(Console.ReadLine());
                                Account account = bank.AccountRepository.GetAccount(id);
                                Console.WriteLine("Enter cash to WithDraw");
                                int cashToDraw = Convert.ToInt32(Console.ReadLine());
                                Request request = new WithDrawRequest(
                                    bank,
                                    Convert.ToInt32(client.PassportId),
                                    id,
                                    cashToDraw);
                                if (client.IsApproved)
                                {
                                    validationHandler.Handle(request);
                                }
                                else
                                {
                                    extraValidationHandler.Handle(request);
                                }

                                break;
                            }
                        }

                        break;
                    }

                    case 4:
                    {
                        Console.WriteLine("Enter bank name");
                        string bankName = Console.ReadLine();
                        Bank bank = CentralBank.BankRepository.GetBank(bankName);
                        foreach (Account currentBank in bank.AccountRepository.GetAll())
                        {
                            Console.WriteLine(currentBank);
                        }

                        break;
                    }
                }
            }
        }
    }
}

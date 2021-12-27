using Banks.Classes;

namespace Banks.Accounts
{
    public class Account
    {
        protected Account(int id, decimal cash, Client client)
        {
            Id = id;
            Cash = cash;
            Client = client;
        }

        public int Id { get; }
        public decimal Cash { get; }
        public Client Client { get; }

        public override string ToString()
        {
            return $"Id: {Id}, Cash: {Cash}, Client: {Client}";
        }

        public virtual void ApplyConditions(Conditions conditions)
        {
        }

        public AccountBuilder ToBuilder()
        {
            return new AccountBuilder()
                .WithCash(Cash)
                .WithClient(Client)
                .WithId(Id);
        }

        public class AccountBuilder
        {
            private int _id;
            private decimal _cash;
            private Client _client;

            public Account Build()
            {
                return new Account(_id, _cash, _client);
            }

            public AccountBuilder WithId(int id)
            {
                _id = id;
                return this;
            }

            public AccountBuilder WithCash(decimal cash)
            {
                _cash = cash;
                return this;
            }

            public AccountBuilder WithClient(Client client)
            {
                _client = client;
                return this;
            }
        }
    }
}
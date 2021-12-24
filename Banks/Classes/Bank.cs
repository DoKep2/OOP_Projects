using Banks.Accounts;
using Banks.Repositories;

namespace Banks.Classes
{
    public class Bank
    {
        public Bank(string name, Conditions conditions, ExtraConditions extraConditions)
        {
            Name = name;
            AccountRepository = new AccountRepository();
            Conditions = conditions;
            ExtraConditions = extraConditions;
        }

        public string Name { get; }
        public IAccountRepository AccountRepository { get; }
        public Conditions Conditions { get; }
        public ExtraConditions ExtraConditions { get; }
        public override string ToString()
        {
            return $"Bank name: {Name}";
        }

        public void ApplyConditions()
        {
            foreach (Account account in AccountRepository.GetAll())
            {
                account.ApplyConditions(Conditions);
            }
        }
    }
}

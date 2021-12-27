using Banks.Classes;

namespace Banks.Accounts
{
    public class CreditAccount : Account
    {
        public CreditAccount(int id, decimal cash, Client client, decimal limit)
            : base(id, cash, client)
        {
            Limit = limit;
        }

        public decimal Limit { get; }
        public override void ApplyConditions(Conditions conditions)
        {
            ToBuilder().WithCash(Cash - ((Cash * Cash < 0 ? conditions.CreditCommission : 0) / 100));
        }
    }
}
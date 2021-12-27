using Banks.Classes;

namespace Banks.Accounts
{
    public class DebitAccount : Account
    {
        public DebitAccount(int id, decimal cash, Client client)
            : base(id, cash, client)
        {
        }

        public override void ApplyConditions(Conditions conditions)
        {
            ToBuilder().WithCash(Cash + (Cash * conditions.DebitPercent / 100)).Build();
        }
    }
}
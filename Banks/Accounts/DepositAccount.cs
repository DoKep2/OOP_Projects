using System;
using Banks.Classes;

namespace Banks.Accounts
{
    public class DepositAccount : Account
    {
        public DepositAccount(int id, decimal cash, Client client, DateTime validity)
            : base(id, cash, client)
        {
            Validity = validity;
        }

        public DateTime Validity { get; }

        public decimal CountCurrentPercent(Conditions conditions)
        {
            decimal currentPercent = decimal.Zero;
            foreach ((decimal cash, decimal percent) in conditions.DepositPercents)
            {
                if (Cash >= cash && currentPercent < percent)
                {
                    currentPercent = percent;
                }
            }

            return currentPercent;
        }

        public override void ApplyConditions(Conditions conditions)
        {
            ToBuilder().WithCash(Cash + (Cash * CountCurrentPercent(conditions) / 100));
        }
    }
}
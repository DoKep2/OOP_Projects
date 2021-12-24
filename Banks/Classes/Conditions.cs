using System.Collections.Generic;

namespace Banks.Classes
{
    public class Conditions
    {
        public Conditions(decimal creditCommission, decimal creditBalanceLimit, decimal debitPercent)
        {
            CreditCommission = creditCommission;
            CreditBalanceLimit = creditCommission;
            DebitPercent = debitPercent;
        }

        public decimal CreditCommission { get; }
        public decimal CreditBalanceLimit { get; }
        public decimal DebitPercent { get; }
        public SortedDictionary<int, decimal> DepositPercents { get; } = new ();
        public void AddPercent(int initialCash, decimal percent)
        {
            DepositPercents[initialCash] = percent;
        }
    }
}
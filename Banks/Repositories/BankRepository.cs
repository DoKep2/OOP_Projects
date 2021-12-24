using System.Collections.Generic;
using System.Linq;
using Banks.Classes;

namespace Banks.Repositories
{
    public class BankRepository : IBankRepository
    {
        private readonly List<Bank> _banks = new ();
        public Bank RegisterBank(Bank bank)
        {
            _banks.Add(bank);
            return bank;
        }

        public List<Bank> GetAll()
        {
            return new List<Bank>(_banks);
        }

        public Bank GetBank(string name)
        {
            return _banks.FirstOrDefault(currentBank => currentBank.Name == name);
        }
    }
}
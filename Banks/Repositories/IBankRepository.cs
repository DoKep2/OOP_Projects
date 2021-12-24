using System.Collections.Generic;
using Banks.Classes;

namespace Banks.Repositories
{
    public interface IBankRepository
    {
        Bank RegisterBank(Bank bank);
        List<Bank> GetAll();
        Bank GetBank(string name);
    }
}
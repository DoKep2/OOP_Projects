using System.Collections.Generic;
using Banks.Classes;

namespace Banks.Repositories
{
    public interface IClientRepository
    {
        Client RegisterClient(Client client);
        List<Client> GetAll();
        Client GetClient(int id);
    }
}
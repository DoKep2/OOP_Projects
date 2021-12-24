using System.Collections.Generic;
using System.Linq;
using Banks.Classes;
using Banks.Exceptions;

namespace Banks.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly List<Client> _clients;

        public ClientRepository()
        {
            _clients = new List<Client>();
        }

        public Client RegisterClient(Client client)
        {
            _clients.Add(client);
            return client;
        }

        public List<Client> GetAll()
        {
            return new List<Client>(_clients);
        }

        public Client GetClient(int id)
        {
            Client client = _clients.FirstOrDefault(currentClient => currentClient.PassportId == id.ToString());
            if (client == null)
            {
                throw new BanksException("Invalid client id");
            }

            return client;
        }
    }
}
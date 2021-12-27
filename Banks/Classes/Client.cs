using Banks.Exceptions;

namespace Banks.Classes
{
    public class Client
    {
        private Client(string name, string surname, string address, string passportId)
        {
            Name = name;
            Surname = surname;
            Address = address;
            PassportId = passportId;
        }

        public string Name { get; }
        public string Surname { get; }
        public string Address { get; }
        public string PassportId { get; }
        public bool IsApproved => Address != null && PassportId != null;
        public ClientBuilder ToBuilder()
        {
            return new ClientBuilder()
                .WithName(Name)
                .WithSurname(Surname)
                .WithAddress(Address)
                .WithPassportId(PassportId);
        }

        public override string ToString()
        {
            return
                $"Name: {Name}, Surname: {Surname}, Address: {Address}, PassportId: {PassportId}, IsApproved: {IsApproved} ";
        }

        public class ClientBuilder
        {
            private string _name;
            private string _surname;
            private string _address;
            private string _passportId;

            public Client Build()
            {
                if (_name == string.Empty || _surname == string.Empty
                || _name == null | _surname == null)
                {
                    throw new BanksException("Fill all required fields");
                }

                return new Client(_name, _surname, _address, _passportId);
            }

            public ClientBuilder WithName(string name)
            {
                _name = name;
                return this;
            }

            public ClientBuilder WithSurname(string surname)
            {
                _surname = surname;
                return this;
            }

            public ClientBuilder WithAddress(string address)
            {
                _address = address;
                return this;
            }

            public ClientBuilder WithPassportId(string passportId)
            {
                _passportId = passportId;
                return this;
            }
        }
    }
}
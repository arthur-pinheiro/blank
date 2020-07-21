using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class Client : BaseEntity
    {
        private Client() { }

        public Client(List<Contact> contacts)
        {
            _contacts = contacts;
        }

        public string Name { get; set; }

        public string Cpf { get; set; }


        private readonly List<Contact> _contacts = new List<Contact>();

        public IReadOnlyCollection<Contact> Contacts => _contacts.AsReadOnly();

        //public ICollection<Contact> Contacts { get; set; }
    }
}

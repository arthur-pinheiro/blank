using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities.Views
{
    public class VP_Client
    {
        private VP_Client() { }


        public string Name { get; set; }

        public string Cpf { get; set; }

        public string LastContact { get; set; }
    }
}

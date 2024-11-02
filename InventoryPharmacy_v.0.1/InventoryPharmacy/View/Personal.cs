using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryPharmacy.View
{
    public class Personal
    {
        public int id { get; set; }
        private string name, login, pass, email, phone, role;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Login
        {
            get { return login; }
            set { login = value; }
        }
        public string Pass
        {
            get { return pass; }
            set { pass = value; }
        }
        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }
        public string Role
        {
            get { return role; }
            set { role = value; }
        }

        public Personal() { }

        public Personal(string name, string login, string pas, string email, string phone, string role)
        {
            this.name = name;
            this.login = login;
            this.pass = pas;
            this.email = email;
            this.phone = phone;
            this.role = role;
        }

    }
}

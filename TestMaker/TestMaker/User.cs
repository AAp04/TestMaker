using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMaker
{
    public class User
    {
        public int id;
        public string username;
        public string password;
        public string useremail;
        public string usertype;
        public User() { }
        public User(string username, string useremail, string userpassword, string usertype)
        {

            this.username = username;
            this.useremail = useremail;
            this.password = userpassword;
            this.usertype = usertype;

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace MobileStore.Models
{
    public class User
    {
        public List<Order> Orders { get; set; } = new List<Order>();
        public int Id { get; set; }
        public string NameAndSurname { get; set; }
        public string Email { get; set; }
        public bool IsVerified { get; set; }
        private string _password;
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                MD5 mD5 = MD5.Create();
                _password = Convert.ToBase64String(mD5.ComputeHash(Convert.FromBase64String(value)));
            }
        }
        public User(string nameAndSurname, string email, string pass)
        {
            NameAndSurname = nameAndSurname;
            Email = email;
            Password = pass;
        }
        public User()
        { }
    }
}

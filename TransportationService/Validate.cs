using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TransportationService
{
    internal class Validate
    {
        private ServiceDBEntities db;
        public Validate(ServiceDBEntities _db)
        {
            db = _db;
            db.Users.ToList();
        }
        public bool checkUserExists(String _login)
        {
            foreach(var u in db.Users)
            {
                if(u.login == _login)
                {
                    return true;
                }
            }
            return false;
        }
        public bool checkDriverExists(String _name, String _surname, DateTime _birthDate)
        {
            foreach(var u in db.Drivers)
            {
                if(u.name == _name && u.surname == _surname && u.birth_date == _birthDate)
                {
                    return true;
                }
            }
            return false;
        }
        public bool checkIfNull(String s)
        {
            if (s.Length == 0)
            {
                return true;
            }         
            return false;
        }
        public bool validatePassword(String password)
        {
            var input = password;

            if (string.IsNullOrWhiteSpace(input))
            {
                throw new Exception("Password should not be empty");
            }

            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMinChars = new Regex(@".{8,}");
            var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

            if (!hasUpperChar.IsMatch(input))
            {
                //MessageBox.Show("Password should contain At least one upper case letter");
                var box = new MsgBox("Password should contain at least one upper case letter");
                box.Show();

                return false;
            }
            else if (!hasMinChars.IsMatch(input))
            {
                //MessageBox.Show("Password should not be less than 8 characters");
                var box = new MsgBox("Password should not be less than 8 characters");
                box.Show();

                return false;
            }
            else if (!hasNumber.IsMatch(input))
            {
                //MessageBox.Show("Password should contain At least one numeric value");
                var box = new MsgBox("Password should contain at least one numeric value");
                box.Show();

                return false;
            }

            else if (!hasSymbols.IsMatch(input))
            {
                //MessageBox.Show("Password should contain At least one special case characters");
                var box = new MsgBox("Password should contain at least one special case\ncharacter");
                box.Show();

                return false;
            }
            else
            {
                return true;
            }
        }


    }
}

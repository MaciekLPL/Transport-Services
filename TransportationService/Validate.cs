using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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


    }
}

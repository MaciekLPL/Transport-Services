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


    }
}

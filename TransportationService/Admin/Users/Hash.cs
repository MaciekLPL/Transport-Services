using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TransportationService {
    internal class Hash {

        public static bool authentication(string _hashedPassword, string _salt, string inputPassword) {
            byte[] bytes = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(inputPassword + _salt));
            var sb = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
                sb.Append(bytes[i].ToString("x2"));
            string hashedPassword = sb.ToString();
            if (hashedPassword == _hashedPassword)
                return true;
            return false;
        }

        public static string generateHash(string passwordInput, string salt) {

            byte[] bytes = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(passwordInput + salt));
            var sb = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
                sb.Append(bytes[i].ToString("x2"));

            return sb.ToString();
        }


        public static string generateSalt() {

            var buff = new byte[32];
            RandomNumberGenerator.Create().GetBytes(buff);
            return Convert.ToBase64String(buff);

        }




    }
}

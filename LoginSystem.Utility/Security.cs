using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Connections;

namespace LoginSystem.Utility
{
    public sealed class Security
    {
        public static string GenerateActivationCode()
        {
            var code = "";

            var stringProvider = "QWERTYUIOPASDFGHJKLZXCVBNM1234567890";

            using (var provider = new RNGCryptoServiceProvider())
            {
                while(code.Length < 6)
                {
                    var oneByte = new Byte[1];
                    provider.GetBytes(oneByte);
                    var character = (char)oneByte[0];
                    if(stringProvider.Contains(character))
                    {
                        code += character;
                    }
                    
                }
            }
            return code;
        }


        public static string GenerateRandomPassword()
        {
            string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*?_-";
            Random random = new Random();
            char [] pswd = new char[10];
            for(int i = 0; i < pswd.Length; i++)
            {
                pswd[i] = validChars[random.Next(0,validChars.Length)];
            }
            return new string(pswd);
        }
    }
}

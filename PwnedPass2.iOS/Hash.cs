using System;
using System.Security.Cryptography;
using System.Text;
using PwnedPass2.Interfaces;

namespace PwnedPass2.iOS
{
    public class Hash : IHash
    {
        

        public string Get(string input)
        {
            using (var sha1 = SHA1.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hash = sha1.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hash.Length; i++)
                {
                    sb.Append(hash[i].ToString("X2"));
                }

                return sb.ToString();
            }
        }
    }
}

using System;
using System.Security.Cryptography;
using System.Text;

namespace ApiPedreirao.Security
{
    public class Seguranca
    {
        public string encriptografar(string senha)
        {
            byte[] senhaOriginal;
            byte[] senhaModificada;
            MD5 mD5;
            senhaOriginal = Encoding.Default.GetBytes(senha);
            mD5 = new MD5CryptoServiceProvider();
            senhaModificada = mD5.ComputeHash(senhaOriginal);

            return Convert.ToBase64String(senhaModificada);

        }
    }
}
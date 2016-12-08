using DevExpress.ExpressApp.DC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SkyFaith.Module.BusinessObjects.DC
{
    [DomainLogic(typeof(IAccount))]
    public class AccountLogic
    {
        public static string GeneratePassword()
        {

            byte[] randomBytes = new byte[5];
            new RNGCryptoServiceProvider().GetBytes(randomBytes);
            return Convert.ToBase64String(randomBytes);
        }

        public static void AfterConstruction(IAccount account)
        {
            account.Password = GeneratePassword();
        }
    }
}

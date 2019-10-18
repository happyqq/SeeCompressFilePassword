using System;
using System.Collections.Generic;
using System.Text;

namespace libcomm
{
    public interface IDataCrypto
    {
    string Encrypt(string encryptString, string encryptKey);
    string Decrypt(string decryptString, string decryptKey);        
    }
}

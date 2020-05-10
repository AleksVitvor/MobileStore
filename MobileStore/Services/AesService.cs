using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace MobileStore.Services
{
    public class AesService
    {
        private static AesService instance;
        public Aes Aes { get; private set; } = Aes.Create();
        private AesService()
        {  }

        public static AesService getInstance()
        {
            if (instance == null)
                instance = new AesService();
            return instance;
        }
    }
}

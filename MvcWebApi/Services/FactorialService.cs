using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcWebApi.Services
{
    public class FactorialService : IFactorialService
    {
        public int MultiplyNumbers(int i, int j)
        {
            return i * j;
        }
    }
}
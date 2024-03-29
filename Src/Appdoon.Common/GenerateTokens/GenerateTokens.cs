﻿using Appdoon.Common.HashFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appdoon.Common.GenerateTokens
{
    public class GenerateTokens
    {
        public static string GenerateToken(string username, string password)
        {
            string temp = username + password;
            //return ArshiaHash.Hash(temp);
            var hash = temp.GetHashCode();
            hash = hash < 0 ? hash * -1 : hash;

            return hash.ToString();
        }
    }
}

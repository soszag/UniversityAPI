﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityAPI.Services.Exceptions
{
    public class RepositoryException : Exception
    {
        public RepositoryException(string message) :
            base(message)
        {
            
        }


    }
}

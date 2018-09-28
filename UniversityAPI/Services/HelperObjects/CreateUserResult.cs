using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityAPI.Dto;

namespace UniversityAPI.Services.HelperObjects
{
    public enum UserCreationError
    {
        NoError,
        LoginAlreadyExists,
        Unknown
    }

    public class CreateUserResult
    {
        public UserDto CreatedUser { get; set; }

        public bool IsCreatedSuccesfully { get; set; }

        public UserCreationError CreationError { get; set; }
    }
}

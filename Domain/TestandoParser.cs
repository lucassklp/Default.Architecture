using Domain.Dtos;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    class TestandoParser
    {
        public void t()
        {
            CredentialsDto credentialsDto = new CredentialsDto();
            credentialsDto.Login = "abcde";
            credentialsDto.Password = "12345";
        }
    }
}

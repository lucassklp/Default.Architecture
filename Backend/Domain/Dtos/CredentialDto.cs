﻿namespace Domain.Dtos
{
    public class CredentialDto : ICredential
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}

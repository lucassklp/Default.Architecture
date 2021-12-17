﻿using System;

namespace Default.Architecture.Services.Exceptions
{
    public class BusinessException : Exception
    {
        public string Token { get; private set; }
        public BusinessException(string msg, string token, Exception ex = null) :
            base(msg, ex)
        {
            this.Token = token;
        }
    }
}

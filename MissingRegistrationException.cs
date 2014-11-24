﻿using System;

namespace MFramework.Infrastructure.ServiceLocator
{
    public class MissingRegistrationException : ApplicationException
    {
        public MissingRegistrationException(Type type) : base("Registration not found for type: " + type.ToString())
        {
        }
    }
}
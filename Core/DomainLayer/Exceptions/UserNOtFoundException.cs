﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Exceptions
{
    public sealed class UserNOtFoundException(string Email) : NotFoundException($"User With Email {Email} is not found.")
    {
    }
}

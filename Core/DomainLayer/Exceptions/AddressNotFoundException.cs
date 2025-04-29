using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Exceptions
{
    public sealed class AddressNotFoundException(string email) : NotFoundException($"Address of User {email} not have Address.")
    {
    }
}

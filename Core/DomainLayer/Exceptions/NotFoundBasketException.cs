using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Exceptions
{
    public sealed class NotFoundBasketException(string id) : NotFoundException($"Basket has Id = {id} is not found.")
    {
    }
}

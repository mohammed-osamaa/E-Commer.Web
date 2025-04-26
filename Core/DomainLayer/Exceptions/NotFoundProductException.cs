using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Exceptions
{
    public sealed class NotFoundProductException(int id) : NotFoundException($"Product with id = {id} not found.")
    {
    }
}

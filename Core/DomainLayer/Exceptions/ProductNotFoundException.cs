using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Exceptions
{
    public sealed class ProductNotFoundException(int id) : NotFoundException($"product With id = {id} is not found ")
    {
        // it is sealed class to prevent any one to inhert from this class
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DataTransferObjects.BsketModuleDTos;

namespace ServiceAbstraction
{
    public interface IPaymentService
    {
        Task<BasketDTo> CreateOrUpdatePaymentIntentAsync(string BasketId);

    }
}

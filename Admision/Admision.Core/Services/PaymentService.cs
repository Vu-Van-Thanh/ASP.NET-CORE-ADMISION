using Admission.Core.Domain.RepositoryContracts;
using Admission.Core.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Core.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRespository;

        public PaymentService(IPaymentRepository paymentRespository)
        {
            _paymentRespository = paymentRespository;
        }

        public async Task<Guid?> GetPaymentByAI(Guid Id)
        {
            return await _paymentRespository.GetPaymetApplied(Id);
        }
    }
}

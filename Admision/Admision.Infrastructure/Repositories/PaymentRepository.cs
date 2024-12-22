using Admission.Core.Domain.Entities;
using Admission.Core.Domain.RepositoryContracts;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Infrastructure.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly ApplicationDbContext _db;

        public PaymentRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Guid?> GetPaymetApplied(Guid AppliedID)
        {
            Payment? payment = await _db.Payments.FirstOrDefaultAsync(p => p.Id == AppliedID);
            if (payment == null) 
            {
                return null;
            }
            else return payment.Id;
        }
    }
}

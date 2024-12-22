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
    public class InfoApplyRepository : IInfoApplyRepository
    {
        private readonly ApplicationDbContext _db;

        public InfoApplyRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<int> AddInfoApply(InformationOfApplied informationOfApplied)
        {
            _db.InformationOfApplieds.Add(informationOfApplied);
            int result = await _db.SaveChangesAsync();
            return result;
        }

        public async Task<List<InformationOfApplied>> GetByEP(string ExamPeriod)
        {
            return  await _db.InformationOfApplieds
                        .Where(ip => ip.ExamPeriod == ExamPeriod)
                        .ToListAsync();
           
        }

        public async Task<List<InformationOfApplied>> GetByStudentId(Guid Id)
        {
            return await _db.InformationOfApplieds.Where(ia=> ia.StudentID == Id).ToListAsync();
        }

        public async Task<List<InformationOfApplied>> UpdateInfo(List<InformationOfApplied> infos)
        {
            List<InformationOfApplied> result = new List<InformationOfApplied> ();
            foreach (InformationOfApplied info in infos) {
                var existing = _db.InformationOfApplieds.FirstOrDefault(i => i.AdmissionMethod == info.AdmissionMethod && i.StudentID == info.StudentID && i.MajorID == info.MajorID);
                if (existing != null)
                {
                    existing.TestDate = info.TestDate;
                    existing.TestRoom = info.TestRoom;
                    result.Add(existing);
                }
            }
            await _db.SaveChangesAsync();
            return result;
        }
    }
}

﻿using Admission.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Core.Domain.RepositoryContracts
{
    public interface IInfoApplyRepository
    {
        Task<int> AddInfoApply(InformationOfApplied informationOfApplied);
        Task<List<InformationOfApplied>> GetByEP(string ExamPeriod);
        Task<List<InformationOfApplied>> GetByStudentId(Guid Id);
        Task<List<InformationOfApplied>> UpdateInfo(List<InformationOfApplied> infos);
    }
}

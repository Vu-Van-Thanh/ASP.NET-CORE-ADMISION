﻿using Admission.Core.Domain.Entities;
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
    public class StudentMediasRepository : IStudentMediasRepository
    {
        private readonly ApplicationDbContext _db;

        public StudentMediasRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<List<StudentMedia>?> GetStudentMediaByStudentID(Guid studentID)
        {
            List<StudentMedia>? mediaList = await _db.StudentMedias
                    .Where(sm => sm.StudentID == studentID).ToListAsync();
            return mediaList;
        }

        public async Task UpdateStudentMedia(List<StudentMedia> medias)
        {
            
            if (medias != null && medias.Any())
            {
                List<StudentMedia> list = await _db.StudentMedias.Where(m => m.StudentID == medias[0].StudentID && m.StudentMediaType == medias[0].StudentMediaType).ToListAsync();
                if(list!= null)
                {
                    _db.StudentMedias.RemoveRange(list);
                }
                await _db.StudentMedias.AddRangeAsync(medias); 
                await _db.SaveChangesAsync(); 
            }
        }

    }
}

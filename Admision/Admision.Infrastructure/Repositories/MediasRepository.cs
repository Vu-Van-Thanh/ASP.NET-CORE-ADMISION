using Admission.Core.Domain.Entities;
using Admission.Core.Domain.RepositoryContracts;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Infrastructure.Repositories
{
    public class MediasRepository : IMediasRepository
    {
        private readonly ApplicationDbContext _db;

        public MediasRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<Media> AddMedia(Media media)
        {
            _db.Medias.Add(media);
            await _db.SaveChangesAsync();
            return media;
        }
    }
}

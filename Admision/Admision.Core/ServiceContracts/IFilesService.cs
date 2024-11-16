using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Core.ServiceContracts
{
    public interface IFilesService
    {
        Task<List<string>> SaveMediaFilesAsync(IFormFile[] mediaFiles, string folderName);
    }
}

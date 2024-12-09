using Admission.Core.Domain.Entities;
using Admission.Core.Domain.RepositoryContracts;
using Admission.Core.DTO;
using Admission.Core.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Core.Services
{
    public class RelativeService : IRelativeService
    {
        private readonly IRelativesRepository _relativesRepository;

        public RelativeService(IRelativesRepository relativesRepository)
        {
            _relativesRepository = relativesRepository;
        }

        public async Task UpdateRelativeShort(List<RelativeShort> list, Guid StudentId)
        {
            List<Relative> reList = await _relativesRepository.GetRelativeByStudentId(StudentId);

            List<Relative> relatives = new List<Relative>();
            foreach (RelativeShort relativeShort in list)
            {
                Relative relative = new Relative();
                relative.StudentID = StudentId;
                relative.RelativeID = Guid.NewGuid();
                relative.RelativeName = relativeShort.RelativeName;
                relative.RelativeType = relativeShort.RelativeType;
                if (relativeShort.DateOfBirth == null)
                {
                    relative.DateOfBirth = null;
                }
                relative.DateOfBirth = DateTime.Parse(relativeShort.DateOfBirth);
                relative.Career = relativeShort.Career;
                relative.PlaceOfJob = relativeShort.PlaceOfJob;
                relative.CountryID = Guid.Parse("4AC574FB-85A9-4159-9F2B-053B2BC6FC7E");
                relatives.Add(relative);
                
            }
            foreach(Relative rel in relatives)
            {
                await _relativesRepository.AddRelative(rel);
            }
            
        }
        public async Task<RelativeDTO> AddRelative(RelativeDTO relativeDTO, Guid studentId)
        {
            List<Relative>? list = await _relativesRepository.GetRelativeByStudentId(studentId);
            if (list != null)
            {
                foreach (Relative relative in list)
                {
                    if(relative.RelativeName == relativeDTO.RelativeName && relative.RelativeType==relativeDTO.RelativeType && relative.DateOfBirth == relativeDTO.DateOfBirth)
                    {
                        return relativeDTO;
                    }
                }
            }

            Relative sample = new Relative();
            sample.DateOfBirth = relativeDTO.DateOfBirth;
            sample.RelativeID = Guid.NewGuid();
            sample.RelativeType = relativeDTO.RelativeType;
            sample.RelativeName = relativeDTO.RelativeName;
            sample.Nationality = relativeDTO.Nationality;
            sample.Ethnic = relativeDTO.Ethnic;
            sample.Religion = relativeDTO.Religion;
            sample.Career = relativeDTO.Career;
            sample.CountryID = Guid.Parse(relativeDTO.Country); 
            sample.PlaceOfJob = relativeDTO.PlaceOfJob;
            sample.Phone = relativeDTO.Phone;
            sample.IdentityCard = relativeDTO.IdentityCard;
            sample.Email = relativeDTO.Email;
            sample.Address = relativeDTO.Address;
            sample.Province = relativeDTO.Province;
            sample.District = relativeDTO.District;
            sample.Commune = relativeDTO.Commune;
            sample.StudentID = studentId;
            await _relativesRepository.AddRelative(sample);
            return relativeDTO;
        }
    }
}

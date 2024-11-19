using Admission.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Core.DTO
{
    public class PostMediaDTO
    {
        public Guid MediaID { get; set; }
        public string Type { get; set; }
        public string MediaUrl { get; set; }

    }

    public static class PostMediaExtension
    {
        public static PostMediaDTO ToPostMediaDTO(this PostMedia media)
        {
            return new PostMediaDTO { MediaID = media.MediaID, Type = media.Type, MediaUrl = media.MediaUrl };
        }
    }

}

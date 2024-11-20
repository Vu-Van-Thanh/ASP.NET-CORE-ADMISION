using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Core.DTO
{
    public class PostData
    {
        public string postId;
        public string author;
        public string timestamp;
        public string? content;
        public List<MediaData> mediaItems = new List<MediaData>();
        public int likes;
        public int comments;
        
    }
    public class MediaData {
        public string type;
        public string src;
    }
}

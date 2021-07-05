using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Model.Models
{
    public class ImageStoreModel
    {


        public Guid ObjectId { get; set; }

        public string ImageExtension { get; set; }
        public string ImageBase64Str { get; set; }
        public byte[] ImageByte { get; set; }
        public string ImagePath { get; set; }
        public Guid Id { get; set; }
        public string ImageName { get; set; }
        public HttpPostedFileWrapper ImageFile { get; set; }
    }
}

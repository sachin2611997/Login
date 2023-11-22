using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Login.Models
{
    public class FileDetailsModel
    {
        public int Id { get; set; }
        public string FileName { get; set; }

        public string Path { get; set; }

        public byte[] FileContent { get; set; }
    }
}
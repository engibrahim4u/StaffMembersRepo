using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.ViewModels
{
    public class FileNameAndBinaryAttachVM
    {
        public string FileName { get; set; }
        public byte[] BinaryAttachentData { get; set; }
    }
}

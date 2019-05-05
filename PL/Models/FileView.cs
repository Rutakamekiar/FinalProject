using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PL.Models
{
    public class FileView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AccessLevel { get; set; }
        public string Path { get; set; }

    }
}
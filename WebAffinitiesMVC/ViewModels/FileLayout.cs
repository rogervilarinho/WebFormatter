using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAffinitiesMVC.Models;
using WebAffinitiesMVC;

namespace WebAffinitiesMVC.ViewModels
{
    public class FileLayout
    {
        public IEnumerable<LAYOUT> Layouts { get; set; }
        public string File { get; set; }
        public string Mensagem { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAffinitiesMVC.Models
{
    public class ArquivoDetalheLayout
    {
        public int id { get; set; }
        public Arquivo arquivo { get; set; }
        public ICollection<ArquivoDetalhe> arquivoDetalhe { get; set; }
        public ICollection<Layout> layoutDetalhe { get; set; }
    }
}
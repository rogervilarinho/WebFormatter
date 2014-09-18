using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAffinitiesMVC.Models
{
    public class LayoutDetalhe
    {
        public int id { get; set; }
        public Layout layout { get; set; }
        public string fixo { get; set; }
        public string campo { get; set; }
        public TipoCampo tipo { get; set; }
        public Hierarquia hierarquia { get; set; }
        public bool obrigatorio { get; set; }
        public int tamanho { get; set; }
        public int inicio { get; set; }
        public int fim { get; set; }
        public Lista lista { get; set; }
        public Validacao validacao { get; set; }
        public string aceitavel { get; set; }
    }
}
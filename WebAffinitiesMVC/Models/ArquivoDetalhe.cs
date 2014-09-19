﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAffinitiesMVC.Models
{
    public class ArquivoDetalhe
    {
        public int id { get; set; }
        public string nome { get; set; }
        public string descricao { get; set; }
        public DateTime criacao { get; set; }
        public DateTime alteracao { get; set; }
        public Arquivo arquivo { get; set; }
    }
}
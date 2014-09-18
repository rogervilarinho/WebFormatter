using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAffinitiesMVC.Models
{
    public class Arquivo
    {
        public int id { get; set; }
        [Display(Name="Nome")]
        public string nome { get; set; }
        [Display(Name="Descrição")]
        public string descricao { get; set; }
    }
}
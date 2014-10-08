//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebAffinitiesMVC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public partial class LISTA
    {
        public LISTA()
        {
            this.LAYOUTDETALHE = new HashSet<LAYOUTDETALHE>();
            this.LISTADETALHE = new HashSet<LISTADETALHE>();
        }
    
        public int ID { get; set; }
        [Display(Name = "Nome")]
        [Required(ErrorMessage = "O campo nome da lista � obrigat�rio!")]
        public string NOME { get; set; }
        [Display(Name = "Descri��o")]
        public string DESCRICAO { get; set; }
        [Display(Name = "Arquivo")]
        [Required(ErrorMessage = "O arquivo da lista � obrigat�rio!")]
        [Range(1, int.MaxValue, ErrorMessage = "O arquivo da lista � obrigat�rio!")]
        public int ID_ARQUIVO { get; set; }
    
        public virtual ARQUIVO ARQUIVO { get; set; }
        public virtual ICollection<LAYOUTDETALHE> LAYOUTDETALHE { get; set; }
        public virtual ICollection<LISTADETALHE> LISTADETALHE { get; set; }
    }
}

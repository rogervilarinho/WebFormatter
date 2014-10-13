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
        public string NOME { get; set; }
        public string DESCRICAO { get; set; }
        public int ID_ARQUIVO { get; set; }
    
        public virtual ARQUIVO ARQUIVO { get; set; }
        public virtual ICollection<LAYOUTDETALHE> LAYOUTDETALHE { get; set; }
        public virtual ICollection<LISTADETALHE> LISTADETALHE { get; set; }
    }
    [MetadataType(typeof(ListaMetaData))]
    public partial class LISTA { }
    public class ListaMetaData
    {
        public int ID { get; set; }
        [Display(Name="Nome")]
        [StringLength(50)]
        [Required(ErrorMessage="O campo nome � obrigat�rio!")]
        public string NOME { get; set; }
        [Display(Name = "Descri��o")]
        [StringLength(200)]
        public string DESCRICAO { get; set; }
        [Display(Name = "Arquivo")]
        [Range(1,Int32.MaxValue, ErrorMessage="O campo arquivo � obrigat�rio!")]
        public int ID_ARQUIVO { get; set; }

        public virtual ARQUIVO ARQUIVO { get; set; }
        public virtual ICollection<LAYOUTDETALHE> LAYOUTDETALHE { get; set; }
        public virtual ICollection<LISTADETALHE> LISTADETALHE { get; set; }
    }
}

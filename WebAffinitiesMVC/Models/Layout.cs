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
    
    public partial class LAYOUT
    {
        public LAYOUT()
        {
            this.ARQUIVODETALHE = new HashSet<ARQUIVODETALHE>();
            this.LAYOUTDETALHE = new HashSet<LAYOUTDETALHE>();
        }
    
        public int ID { get; set; }
        public string NOME { get; set; }
        public string DESCRICAO { get; set; }
        public int ID_ARQUIVO { get; set; }
    
        public virtual ARQUIVO ARQUIVO { get; set; }
        public virtual ICollection<ARQUIVODETALHE> ARQUIVODETALHE { get; set; }
        public virtual ICollection<LAYOUTDETALHE> LAYOUTDETALHE { get; set; }
    }
}

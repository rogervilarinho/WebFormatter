﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class WebFormatterEntities : DbContext
    {
        public WebFormatterEntities()
            : base("name=WebFormatterEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ARQUIVO> ARQUIVO { get; set; }
        public virtual DbSet<ARQUIVODETALHE> ARQUIVODETALHE { get; set; }
        public virtual DbSet<LAYOUT> LAYOUT { get; set; }
        public virtual DbSet<LAYOUTDETALHE> LAYOUTDETALHE { get; set; }
        public virtual DbSet<LISTA> LISTA { get; set; }
        public virtual DbSet<LISTADETALHE> LISTADETALHE { get; set; }
        public virtual DbSet<TIPO> TIPO { get; set; }
        public virtual DbSet<VALIDACAO> VALIDACAO { get; set; }
    }
}

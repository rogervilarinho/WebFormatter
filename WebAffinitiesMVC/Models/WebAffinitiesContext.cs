using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace WebAffinitiesMVC.Models
{
    public class WebAffinitiesContext : DbContext
    {
        public WebAffinitiesContext()
            : base("WebAffinitiesContext")
        {
        }

        public DbSet<Arquivo> Arquivos { get; set; }
        public DbSet<ArquivoDetalhe> ArquivoDetalhes { get; set; }
        public DbSet<Lista> Listas { get; set; }
        public DbSet<Validacao> Validacoes { get; set; }
        public DbSet<TipoCampo> TipoCampos { get; set; }
        public DbSet<LayoutDetalhe> LayoutDetalhes { get; set; }
        public DbSet<Layout> Layouts { get; set; }
        public DbSet<Hierarquia> Hierarquias { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
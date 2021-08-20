using Microsoft.EntityFrameworkCore;

namespace CadastroProdutos.API.Models
{
    public class DataContext : DbContext
    {
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Produto> Produtos { get; set; }
    }
}

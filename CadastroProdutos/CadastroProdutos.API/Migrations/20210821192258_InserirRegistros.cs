using Microsoft.EntityFrameworkCore.Migrations;

namespace CadastroProdutos.API.Migrations
{
    public partial class InserirRegistros : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Insert into Categorias(Nome, DataInclusao, Ativo) Values('Bebidas',now(), 1)");
            migrationBuilder.Sql("Insert into Categorias(Nome, DataInclusao, Ativo) Values('Lanches',now(), 1)");
            migrationBuilder.Sql("Insert into Categorias(Nome, DataInclusao, Ativo) Values('Sobremesas',now(), 1)");

            migrationBuilder.Sql("Insert into Produtos(Nome, Descricao, Preco, ImagemUrl, Estoque, DataInclusao, Ativo, CategoriaId) " +
                                "Values('Coca-Cola', 'Refrigerante de Cola 350 ml', 5.45, 'https://i.postimg.cc/2y5mHH4Q/coca.jpg', 50, now(), 1, (Select CategoriaId from Categorias where Nome='Bebidas'))");

            migrationBuilder.Sql("Insert into Produtos(Nome, Descricao, Preco, ImagemUrl, Estoque, DataInclusao, Ativo, CategoriaId) " +
                                "Values('Lanche de Atum', 'Lanche de Atum com maionese', 8.50, 'https://i.postimg.cc/W1dy5Bxp/atum.jpg', 10, now(), 1, (Select CategoriaId from Categorias where Nome='Lanches'))");

            migrationBuilder.Sql("Insert into Produtos(Nome, Descricao, Preco, ImagemUrl, Estoque, DataInclusao, Ativo, CategoriaId) " +
                                "Values('Pudim 100 g', 'Pudim de leite condensado 100g', 6.75, 'https://i.postimg.cc/FHwB8PQy/pudim.jpg', 20, now(), 1, (Select CategoriaId from Categorias where Nome='Sobremesas'))");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}

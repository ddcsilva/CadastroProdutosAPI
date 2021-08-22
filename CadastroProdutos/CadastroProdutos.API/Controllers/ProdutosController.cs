using CadastroProdutos.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CadastroProdutos.API.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly DataContext context;

        // Injeção de Dependência do DataContext
        public ProdutosController(DataContext context)
        {
            this.context = context;
        }

        // Action para listar todos os Produtos, sem restrições
        [HttpGet]
        public ActionResult<IEnumerable<Produto>> Get()
        {
            // Utilização do Método AsNoTracking() para cancelar o rastreamento de estado
            return this.context.Produtos.AsNoTracking().ToList();
        }

        // Action para listar um Produto pelo seu Id
        [HttpGet("{id}", Name = "ObterProduto")]
        public ActionResult<Produto> Get(int id)
        {
            Produto produto = this.context.Produtos.AsNoTracking().FirstOrDefault(p => p.ProdutoId == id);

            // Se não encontrar um produto, retorna um código 404 (Not Found)
            if (produto == null)
                return NotFound();

            return produto;
        }

        // Action para incluir um novo Produto com dados recebidos no corpo da requisição
        [HttpPost]
        public ActionResult Post([FromBody] Produto produto)
        {
            // Adiciona o objeto na memória
            this.context.Add(produto);
            // Persiste no banco de dados
            this.context.SaveChanges();

            // Retorna o código 201 (Created) juntamente com a chamada de outra Action através da rota
            return new CreatedAtActionResult("Get", "Produtos", new { id = produto.ProdutoId }, produto);
        }

        [HttpPut("{id}")]
        public ActionResult Put([FromBody] Produto produto, int id)
        {
            if (id != produto.ProdutoId)
                return BadRequest();

            var produtoBD = this.context.Produtos.AsNoTracking().FirstOrDefault(p => p.ProdutoId == id);

            if (produtoBD == null)
                return NotFound();

            this.context.Entry(produto).State = EntityState.Modified;
            this.context.SaveChanges();

            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult<Produto> Delete(int id)
        {
            Produto produto = this.context.Produtos.AsNoTracking().FirstOrDefault(p => p.ProdutoId == id);

            // Se não encontrar um produto, retorna um código 404 (Not Found)
            if (produto == null)
                return NotFound();

            this.context.Produtos.Remove(produto);
            this.context.SaveChanges();

            return produto;
        }
    }
}

using CadastroProdutos.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CadastroProdutos.API.Controllers
{
    [Route("api/{Controller}")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly DataContext context;

        public CategoriasController(DataContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Categoria>> Get()
        {
            try
            {
                return this.context.Categorias.AsNoTracking().ToList();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro ao tentar obter as categorias do Banco de Dados!");
            }
        }

        [HttpGet("Produtos")]
        public ActionResult<IEnumerable<Categoria>> GetCategoriasProdutos()
        {
            return this.context.Categorias.Include(c => c.Produtos).ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Categoria> Get(int id)
        {
            try
            {
                var categoria = this.context.Categorias.AsNoTracking().FirstOrDefault(c => c.CategoriaId == id);

                if (categoria == null)
                    return NotFound($"A categoria com id = {id} não foi encontrada!");

                return categoria;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro ao tentar obter as categorias do Banco de Dados!");
            }
        }

        [HttpPost]
        public ActionResult Post([FromBody] Categoria categoria)
        {
            try
            {
                this.context.Categorias.Add(categoria);
                this.context.SaveChanges();

                return new CreatedAtActionResult("Get", "Categorias", new { id = categoria.CategoriaId }, categoria);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro ao tentar criar uma nova categoria!");
            }
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Categoria categoria)
        {
            try
            {
                if (id != categoria.CategoriaId)
                    return BadRequest($"Não foi possível atualizar a categoria com id = {id}");

                var categoriaDB = this.context.Categorias.AsNoTracking().FirstOrDefault(c => c.CategoriaId == id);

                if (categoriaDB == null)
                    return NotFound($"A categoria com id = {id} não foi encontrada!");

                this.context.Entry(categoria).State = EntityState.Modified;
                this.context.SaveChanges();

                return Ok($"A categoria com id = {id} foi atualizada com sucesso!");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao atualizar a categoria com id = {id}");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<Categoria> Delete(int id)
        {
            try
            {
                var categoria = this.context.Categorias.AsNoTracking().FirstOrDefault(c => c.CategoriaId == id);

                if (categoria == null)
                    return NotFound($"A categoria com id = {id} não foi encontrada");

                this.context.Categorias.Remove(categoria);
                this.context.SaveChanges();

                return categoria;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao excluir a categoria com id = {id}");
            }
        }
    }
}

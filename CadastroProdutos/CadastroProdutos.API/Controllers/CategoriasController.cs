using CadastroProdutos.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            return this.context.Categorias.AsNoTracking().ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Categoria> Get(int id)
        {
            var categoria = this.context.Categorias.AsNoTracking().FirstOrDefault(c => c.CategoriaId == id);

            if (categoria == null)
                return NotFound();

            return categoria;
        }

        [HttpPost]
        public ActionResult Post([FromBody]Categoria categoria)
        {
            this.context.Categorias.Add(categoria);
            this.context.SaveChanges();

            return new CreatedAtActionResult("Get", "Categorias", new { id = categoria.CategoriaId }, categoria);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody]Categoria categoria)
        {
            if (id != categoria.CategoriaId)
                return BadRequest();

            var categoriaDB = this.context.Categorias.AsNoTracking().FirstOrDefault(c => c.CategoriaId == id);

            if (categoriaDB == null)
                return NotFound();

            this.context.Entry(categoria).State = EntityState.Modified;
            this.context.SaveChanges();

            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult<Categoria> Delete(int id)
        {
            var categoria = this.context.Categorias.AsNoTracking().FirstOrDefault(c => c.CategoriaId == id);

            if (categoria == null)
                return NotFound();

            this.context.Categorias.Remove(categoria);
            this.context.SaveChanges();

            return categoria;
        }
    }
}

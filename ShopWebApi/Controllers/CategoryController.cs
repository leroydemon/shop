using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopWebApi.Data;
using System.Collections;
using System.Numerics;

namespace ShopWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpPost]
        public IActionResult Post([FromBody] Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            _db.Categories.Add(category);
            _db.SaveChanges();
            return Ok();
        }
        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            var item = _db.Categories.Find(id);
            if(item == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(item);
            }
        }
        [HttpPut]
        public IActionResult Update(int id,[FromForm] Category category)
        {
            var item = _db.Categories.Find(id);
            if(item == null)
            {
                return NotFound();
            }
            item.Quantity = category.Quantity;
            item.Description = category.Description;
            item.Name = category.Name;
            _db.SaveChanges();
            return Ok(category);
        }
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var item = _db.Categories.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            _db.Categories.Remove(item);
            _db.SaveChanges();
            return Ok();
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using webAPI.Data;
using webAPI.Models;

namespace webAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FurnitureControler:Controller
    {
        private DatabaseContext _context;

        public FurnitureControler(DatabaseContext context)
        {
            _context=context;
        }
        [HttpGet]
        public async Task<ActionResult<List<Furniture>>> getFurniture()
        {
            var furniture = await _context.furnitures.ToListAsync();
            return furniture;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Furniture>> GetFurnitureByID(int id)
        {
            var furniture = await _context.furnitures.FindAsync(id);
            if(furniture==null)
            {
                return NotFound();
            }
            return furniture;
        }

        [HttpPost]
        public async Task<ActionResult<Furniture>> PostFurniture(Furniture furniture)
        {
            _context.furnitures.Add(furniture);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetFurnitureByID", new{id=furniture.FurId}, furniture);
        }

        [HttpPut]
        public async Task<ActionResult<Furniture>> PutFurniture(int id, Furniture furniture)
        {
            if(id != furniture.FurId)
            {
                return BadRequest();
            }
            _context.Entry(furniture).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                if(!FurnitureExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction("GetFurnitureByID", new{id=furniture.FurId}, furniture);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Furniture>> DeleteFurniture(int id)
        {
            var furniture = await _context.furnitures.FindAsync(id);
            if(furniture==null)
            {
                return NotFound();
            }

            _context.furnitures.Remove(furniture);
            await _context.SaveChangesAsync();

            return furniture;
        }

        private bool FurnitureExists(int id)
        {
            return _context.furnitures.Any(f=>f.FurId==id);
        }
    }
}





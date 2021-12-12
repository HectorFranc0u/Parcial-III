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
    ///<summary>
    /// Web API para gestionar muebles.
    ///</summary>
    [ApiController]
    [Route("Furniture")]
    public class FurnitureControler:Controller
    {
        private DatabaseContext _context;

        public FurnitureControler(DatabaseContext context)
        {
            _context=context;
        }

        ///<summary>
        /// Get all the pieces of furniture registered.
        ///</summary>
         ///<remarks>
        /// With this function you can see the pieces of furniture in your database 😼
        ///</remarks>
        ///<response code="200">SUCCESS. Now you can watch the items on your database 😸 </response>
        ///<response code="404">ERROR. There's nothing here! 🙀 </response>
        [HttpGet]
        public async Task<ActionResult<List<Furniture>>> getFurniture()
        {
            var furniture = await _context.furnitures.ToListAsync();
            return furniture;
        }

        ///<summary>
        /// Get a piece of furniture by the id.
        ///</summary>
        ///<remarks>
        /// You can find an item on your database just by adding the id 🐱
        ///</remarks>
        ///<response code="200">SUCCESS. This is what you were looking for? 😼 </response>
        ///<response code="404">ERROR. We couldn't found the item on the database 🙀 </response>
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

        ///<summary>
        /// Register a new piece of Furniture.
        ///</summary>
        ///<remarks>
        /// With this function you can add new pieces of furniture to your database 😼
        ///</remarks>
        ///<response code="200">SUCCESS. Your item has been successfully added to the database 😸 </response>
        ///<response code="404">ERROR. Your item couldn't be added to the database 🙀 </response>
        [HttpPost]
        public async Task<ActionResult<Furniture>> PostFurniture(Furniture furniture)
        {
            _context.furnitures.Add(furniture);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetFurnitureByID", new{id=furniture.FurId}, furniture);
        }

        ///<summary>
        /// Edit a piece of Furniture in your database.
        ///</summary>
        ///<remarks>
        /// Please type the id of the item you want to edit please 🐱
        ///</remarks>
        ///<response code="200">SUCCESS. Your item has been successfully edited 😼 </response>
        ///<response code="404">ERROR. We couldn't found the item on the database 🙀 </response>
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

        ///<summary>
        /// Delete a piece of Furniture in your database.
        ///</summary>
        ///<remarks>
        /// Please type the id of the item you want to delete please 🐱
        ///</remarks>
        ///<response code="200">SUCCESS. Nothing ever happened here! 😼 </response>
        ///<response code="404">ERROR. We couldn't found the item on the database 🙀 </response>
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

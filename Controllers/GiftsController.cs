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
    /// Web API para gestionar regalos incluidos en la compra de un mueble.
    ///</summary>
    [ApiController]
    [Route("[controller]")]
    public class GiftsController:Controller
    {
        private DatabaseContext _context;

        public GiftsController(DatabaseContext context)
        {
            _context=context;
        }

        ///<summary>
        /// Get all the gifts registered.
        ///</summary>
        ///<remarks>
        /// You can find all the gifts added on the buy of a piece of furniture 😼
        ///</remarks>
        ///<response code="200">SUCCESS. These are all the gifts available now 😸 </response>
        ///<response code="404">ERROR. There's nothing here! 🙀 </response>
        [HttpGet]
        public async Task<ActionResult<List<Gift>>> getGifts()
        {
            var gifts = await _context.gift.ToListAsync();
            return gifts;
        }

        ///<summary>
        /// Find a gift by its id.
        ///</summary>
        ///<remarks>
        /// You can find all the gifts added on the buy of a piece of furniture by their ID 😼
        ///</remarks>
        ///<response code="200">SUCCESS. Here's the gift, they'll love it! 😸 </response>
        ///<response code="404">ERROR. We couldn't find the gift! 😿 </response>
        [HttpGet("{id}")]
        public async Task<ActionResult<Gift>> GetGiftByID(int id)
        {
            var gifts = await _context.gift.FindAsync(id);
            if(gifts==null)
            {
                return NotFound();
            }
            return gifts;
        }

        ///<summary>
        /// Register a new gift.
        ///</summary>
        ///<remarks>
        /// With this function you can add new gifts to your database 😼
        ///</remarks>
        ///<response code="200">SUCCESS. Your gift has been successfully added to the database 😸 </response>
        ///<response code="404">ERROR. Your gift couldn't be added to the database 🙀 </response>
        [HttpPost]
        public async Task<ActionResult<Gift>> PostGift(Gift gifts)
        {
            _context.gift.Add(gifts);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetGiftByID", new{id=gifts.GiftID}, gifts);
        }

        ///<summary>
        /// Edit the data of a gift in your database.
        ///</summary>
        ///<remarks>
        /// Please type the id of the gift you want to edit please 🐱
        ///</remarks>
        ///<response code="200">SUCCESS. Your gift has been successfully edited 😼 </response>
        ///<response code="404">ERROR. We couldn't found the item on the database 🙀 </response>
        [HttpPut]
        public async Task<ActionResult<Gift>> PutGift(int id, Gift gift)
        {
            if(id != gift.GiftID)
            {
                return BadRequest();
            }
            _context.Entry(gift).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                if(!GiftExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction("GetGifteByID", new{id=gift.GiftID}, gift);
        }

        ///<summary>
        /// Delete a gift in your database.
        ///</summary>
        ///<remarks>
        /// Please type the id of the gift you want to delete please 🐱
        ///</remarks>
        ///<response code="200">SUCCESS. Nothing ever happened here! 😼 </response>
        ///<response code="404">ERROR. We couldn't found the gift on the database 🙀 </response>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Gift>> DeleteGift(int id)
        {
            var gifts = await _context.gift.FindAsync(id);
            if(gifts==null)
            {
                return NotFound();
            }

            _context.gift.Remove(gifts);
            await _context.SaveChangesAsync();

            return gifts;
        }

        private bool GiftExists(int id)
        {
            return _context.gift.Any(g=>g.GiftID==id);
        }
    }
}

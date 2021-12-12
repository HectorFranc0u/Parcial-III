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
    public class GiftsController:Controller
    {
        private DatabaseContext _context;

        public GiftsController(DatabaseContext context)
        {
            _context=context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Gift>>> getGifts()
        {
            var gifts = await _context.gift.ToListAsync();
            return gifts;
        }

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

        [HttpPost]
        public async Task<ActionResult<Gift>> PostGift(Gift gifts)
        {
            _context.gift.Add(gifts);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetGiftByID", new{id=gifts.GiftID}, gifts);
        }

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

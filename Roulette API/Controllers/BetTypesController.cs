using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Roulette_API.Context;
using Roulette_API.Models;

namespace Roulette_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BetTypesController : ControllerBase
    {
        private readonly RouletteContext _context;

        public BetTypesController(RouletteContext context)
        {
            _context = context;
        }

        // GET: api/BetTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BetType>>> GetBetTypes()
        {
          if (_context.BetTypes == null)
          {
              return NotFound();
          }
            return await _context.BetTypes.ToListAsync();
        }

        // GET: api/BetTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BetType>> GetBetType(int id)
        {
          if (_context.BetTypes == null)
          {
              return NotFound();
          }
            var betType = await _context.BetTypes.FindAsync(id);

            if (betType == null)
            {
                return NotFound();
            }

            return betType;
        }

        // PUT: api/BetTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBetType(int id, BetType betType)
        {
            if (id != betType.BetTypeID)
            {
                return BadRequest();
            }

            _context.Entry(betType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BetTypeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/BetTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BetType>> PostBetType(BetType betType)
        {
          if (_context.BetTypes == null)
          {
              return Problem("Entity set 'RouletteContext.BetTypes'  is null.");
          }

            betType.BetTypeID = (int)DatabaseGeneratedOption.Identity;
            _context.BetTypes.Add(betType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBetType", new { id = betType.BetTypeID }, betType);
        }

        // DELETE: api/BetTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBetType(int id)
        {
            if (_context.BetTypes == null)
            {
                return NotFound();
            }
            var betType = await _context.BetTypes.FindAsync(id);
            if (betType == null)
            {
                return NotFound();
            }

            _context.BetTypes.Remove(betType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BetTypeExists(int id)
        {
            return (_context.BetTypes?.Any(e => e.BetTypeID == id)).GetValueOrDefault();
        }
    }
}

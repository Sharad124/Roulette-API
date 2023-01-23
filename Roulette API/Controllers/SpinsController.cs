using System;
using System.Collections.Generic;
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
    public class SpinsController : ControllerBase
    {
        private readonly RouletteContext _context;

        public SpinsController(RouletteContext context)
        {
            _context = context;
        }

        // GET: api/Spins
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Spin>>> Getspins()
        {
          if (_context.spins == null)
          {
              return NotFound();
          }
            return await _context.spins.ToListAsync();
        }

        // GET: api/Spins/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Spin>> GetSpin(int id)
        {
          if (_context.spins == null)
          {
              return NotFound();
          }
            var spin = await _context.spins.FindAsync(id);

            if (spin == null)
            {
                return NotFound();
            }

            return spin;
        }

       
        


    [HttpPost("Spin")]
    public async Task<ActionResult<Spin>> Spin(Spin spin)
    {
      Random r = new Random();

      int number = r.Next(-1, 33);

      if (number % 2 == 0)
      {
        spin.isEven = true;
        spin.isRed = true;

        spin.isGreen = false;
        spin.isOdd = false;
        spin.isBlack = false;

        if (number > 0 && number <= 18)
        {
          spin.isNumberRange1 = true;
          spin.isNumberRange2 = false;
        }

        spin.Result = number + " Red";

        _context.spins.Add(spin);
        await _context.SaveChangesAsync();

        return CreatedAtAction("SpinRoulette", new { id = spin.SpinID }, spin);
      }
      else if (number == 0)
      {
        spin.isGreen = true;
        spin.Result = number + " Green";

        spin.isGreen = false;
        spin.isRed = false;
        spin.isEven = false;
        spin.isOdd = false;
        spin.isBlack = false;
        spin.isNumberRange1 = false;
        spin.isNumberRange2 = false;

        _context.spins.Add(spin);
        await _context.SaveChangesAsync();

        return CreatedAtAction("SpinRoulette", new { id = spin.SpinID }, spin);
      }
      else
      {
        spin.isOdd = true;
        spin.isBlack = true;

        spin.isGreen = false;
        spin.isRed = false;
        spin.isEven = false;

        if (number > 18 && number <= 36)
        {
          spin.isNumberRange2 = true;
          spin.isNumberRange1 = false;
        }

        spin.Result = number + " Black";

        _context.spins.Add(spin);
        await _context.SaveChangesAsync();

        return CreatedAtAction("SpinRoulette", new { id = spin.SpinID }, spin);
      }
    }

    private bool SpinExists(int id)
        {
            return (_context.spins?.Any(e => e.SpinID == id)).GetValueOrDefault();
        }
    }
}

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
using Microsoft.Data.Sqlite;

namespace Roulette_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BetsController : ControllerBase
    {
        private readonly RouletteContext _context;

        public BetsController(RouletteContext context)
        {
            _context = context;
        }

        // GET: api/Bets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bet>>> GetBets()
        {
          if (_context.Bets == null)
          {
              return NotFound();
          }
            return await _context.Bets.ToListAsync();
        }

        // GET: api/Bets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Bet>> GetBet(int id)
        {
          if (_context.Bets == null)
          {
              return NotFound();
          }
            var bet = await _context.Bets.FindAsync(id);

            if (bet == null)
            {
                return NotFound();
            }

            return bet;
        }

        

    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpGet("CalculatePotentialPayout")]
    public decimal CalculatePotentialPayout(decimal betAmount, int BetTypeID)
    {
      decimal Payout = 0;

      switch (BetTypeID)
      {
        case 1:
          Payout = betAmount * 2;
          break;
        case 2:
          Payout = betAmount * 3;
          break;
        case 3:
          Payout = betAmount * 4;
          break;
        case 4:
          Payout = betAmount * 4;
          break;
        case 5:
          Payout = betAmount * 5;
          break;
      }

      if(Payout < 0)
      {
        Payout = 0;
      }
      return Payout;
    }

    // POST: api/Bets
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost("PlaceBet")]
        public async Task<ActionResult<Bet>> PlaceBet(Bet bet)
        {
          string[] BetValues = { "Number", "Colour", "Odd", "Even", "Number Range" };

          if (_context.Bets == null)
          {
              return Problem("Entity set 'RouletteContext.Bets'  is null.");
          }

          if (bet.UserID <= 0 || bet.BetTypeID <= 0)
          {
            return BadRequest();
          }

          for (int i = 0; i < BetValues.Length; i++)
          {
            if (bet.BetItem.Contains(BetValues[i]))
            {
              bet.PotentialPayout = CalculatePotentialPayout(bet.BetAmount, i);
            }
          }

      bet.BetID = (int)DatabaseGeneratedOption.Identity;
            bet.isActive = true;
            _context.Bets.Add(bet);

      //string cs = "Data Source=:memory:";

      //using var con = new SqliteConnection(cs);

      //con.Open();

      //using var cmd = new SqliteCommand("", con);

      //cmd.CommandText = "CREATE TABLE User(UserID INT PRIMARY KEY, " +
      //  "FirstName VARCHAR, " +
      //  "LastName VARCHAR)";
      //cmd.ExecuteNonQuery();

      //cmd.CommandText = "CREATE TABLE BetType(BetTypeID INT PRIMARY KEY, " +
      //  "Name VARCHAR)";
      //cmd.ExecuteNonQuery();

      //cmd.CommandText = "CREATE TABLE Bet(BetID INT PRIMARY KEY, " +
      //  "BetAmount INT, " +
      //  "BetItem VARCHAR, " +
      //  "PotentialPayout DECIMAL, " +
      //  "isActive BIT, " + 
      //  "FOREIGN KEY (UserID) References User(UserID)" +
      //  "FOREIGN KEY (BetTypeID) References BetType(BetTypeID))";
      //cmd.ExecuteNonQuery();

      //cmd.CommandText = "INSERT INTO Bet Values(" + bet.BetID + ", " + bet.BetAmount + ", " + bet.BetItem + ", " + bet.PotentialPayout + ", " + bet.isActive + ", " + bet.UserID + ", " + bet.BetTypeID + ")";
      //var response = cmd.ExecuteNonQuery();

      await _context.SaveChangesAsync();

            return CreatedAtAction("GetBet", new { id = bet.BetID }, bet);
        }

       

        private bool BetExists(int id)
        {
            return (_context.Bets?.Any(e => e.BetID == id)).GetValueOrDefault();
        }
    }
}

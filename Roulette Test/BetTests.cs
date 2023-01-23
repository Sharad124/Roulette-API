using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Roulette_API.Context;
using Roulette_API.Controllers;
using Roulette_API.Models;

namespace Roulette_Test
{
    [TestClass]
    public class BetTests
    {
        private readonly RouletteContext _context;

        [TestMethod]
        public async Task PlaceBet_WithInvalidUserID_BadRequestAsync()
        {
          // Arrange
          BetsController betsController = new BetsController(_context);
          Bet bet = new Bet();    

          bet.BetID = 3;
          bet.BetAmount = 500;
          bet.BetItem = "Number";
          bet.BetTypeID = 1;
          bet.UserID = -1;
          bet.PotentialPayout = 1000;
          bet.isActive = true;

          _context.Bets.Add(bet);
          await _context.SaveChangesAsync();

          // Act
          var actual = await betsController.PlaceBet(bet);

          // Assert
          var expected = StatusCodes.Status400BadRequest;
          Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public async Task PlaceBet_WithNullValues_ThrowsNullException()
        {
          BetsController betsController = new BetsController(_context);

          Bet bet = new Bet();
            _ = _context.Bets == null;

          var actual = await betsController.PlaceBet(bet);
        }

        [TestMethod]
        public async Task PlaceBet_WithValidValues_Returns201Created()
        {
          BetsController betsController = new BetsController(_context);

          Bet bet = new Bet();
          
          bet.BetID = 3;
          bet.BetAmount = 500;
          bet.BetItem = "Number";
          bet.BetTypeID = 1;
          bet.UserID = 1;
          bet.PotentialPayout = 1000;
          bet.isActive = true;

          _context.Bets.Add(bet);
          await _context.SaveChangesAsync();

          var actual = await betsController.PlaceBet(bet);

          var expected = StatusCodes.Status201Created;

          Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CalculatePotentialPayout_WithValidValues_ReturnAmountGraterThanZero()
        {
          BetsController betsController = new BetsController(_context);

          Assert.AreEqual(1000,
          betsController.CalculatePotentialPayout(500,1));
        }

        [TestMethod]
        public void CalculatePotentialPayout_WithInValidValues_ReturnZero()
        {
          BetsController betsController = new BetsController(_context);

          Assert.AreEqual(0,
          betsController.CalculatePotentialPayout(-1, 1));
        }
  }
}
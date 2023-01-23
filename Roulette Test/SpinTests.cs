using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Roulette_API.Context;
using Roulette_API.Controllers;
using Roulette_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roulette_Test
{
  [TestClass]
    public class SpinTests
    {
      private readonly RouletteContext _context;

      [TestMethod]
      public void Spin_CallMethod_Return201CreatedResponse()
      {
          SpinsController spinsController = new SpinsController(_context);

          Spin spin = new Spin();

          Assert.AreEqual(StatusCodes.Status201Created, spinsController.Spin(spin));
      }

    }
}

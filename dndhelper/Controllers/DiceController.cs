using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using dndhelper.Data;
using dndhelper.Models;

namespace dndhelper.Controllers
{
    public class DiceController : Controller
    {
        public IActionResult Index()
        {
            var diceModel = new DiceModel();
            return View(diceModel);
        }

        [HttpPost]
        public async Task<IActionResult> RollDice(DiceModel model)
        {
            var maxNumber = (int)model.SelectedDice;

            var random = new Random();

            model.RandomNumber = random.Next(1, maxNumber + 1);
            ModelState.Clear(); // Wyczyść stan modelu

            return View("Index", model);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dndhelper.Data;
using dndhelper.Models;
using Microsoft.AspNetCore.Authorization;

namespace dndhelper.Controllers
{
    [Authorize]
    public class EventsController : Controller
    {
        private readonly dndhelperContext _context;

        public EventsController(dndhelperContext context)
        {
            _context = context;
        }

        // GET: Events/Index
        // GET: Events/Index
        public IActionResult Index(int campaignId, string campaignName)
        {
            ViewBag.CampaignId = campaignId;
            ViewBag.CampaignName = campaignName;

            var events = _context.Event.Where(e => e.CampaignId == campaignId).ToList();

            return View(events);
        }


        // GET: Events/Create
        public IActionResult Create(int campaignId)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var campaign = _context.Campaign.FirstOrDefault(c => c.Id == campaignId && c.UserId == userId);

            if (campaign == null)
            {
                return NotFound();
            }

            var @event = new Event
            {
                CampaignId = campaignId,
                Strength = GetRandomStatValue(),
                Dexterity = GetRandomStatValue(),
                Intelligence = GetRandomStatValue(),
                Luck = GetRandomStatValue()
            };

            return View(@event);
        }

        // POST: Events/Create
        [HttpPost]
     
        public IActionResult Create([Bind("Id,Name,Strength,Dexterity,Intelligence,Luck,CampaignId")] Event @event)
        {
            if (ModelState.IsValid)
            {
                _context.Add(@event);
                _context.SaveChanges();
                return RedirectToAction("Index", "Events", new { campaignId = @event.CampaignId });
            }

            return View(@event);
        }

        // ... reszta kodu ...

        private int GetRandomStatValue()
        {
            // Implementacja losowego generowania wartości statystyki (np. od 1 do 20)
            Random random = new Random();
            return random.Next(1, 21);
        }
    }
}

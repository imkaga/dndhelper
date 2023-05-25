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
        public IActionResult Index(int campaignId, string campaignName)
        {
            ViewBag.CampaignId = campaignId;
            ViewBag.CampaignName = campaignName;

            try
            {
                var events = _context.Event.Where(e => e.CampaignId == campaignId).ToList();
                return View(events);
            }
            catch (Exception e)
            {
                // Handle the exception (e.g., log the error)
                // You can customize the error handling based on your requirements
                // Redirect to an error page or display an error message
                ViewBag.ErrorMessage = "An error occurred while retrieving events.";
                return View();
            }
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

            try
            {
                var @event = new Event
                {
                    CampaignId = campaignId,
                    UserId = userId,
                    Strength = GetRandomStatValue(),
                    Dexterity = GetRandomStatValue(),
                    Intelligence = GetRandomStatValue(),
                    Luck = GetRandomStatValue()
                };

                return View(@event);
            }
            catch (Exception e)
            {
                // Handle the exception (e.g., log the error)
                // You can customize the error handling based on your requirements
                // Redirect to an error page or display an error message
                ViewBag.ErrorMessage = "An error occurred while creating the event.";
                return View();
            }
        }

        // POST: Events/Create
        [HttpPost]
        public IActionResult Create(Event @event)
        {
            if (ModelState.IsValid)
            {
                _context.Add(@event);
                _context.SaveChanges();

                var campaign = _context.Campaign.FirstOrDefault(c => c.Id == @event.CampaignId);
                if (campaign != null)
                {
                    return RedirectToAction("Index", "Events", new { campaignId = @event.CampaignId, campaignName = campaign.Name });
                }
            }

            return View(@event);
        }

        // GET: Events/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Event.FirstOrDefaultAsync(m => m.Id == id);
            if (@event == null)
            {
                return NotFound();
            }

            var campaign = await _context.Campaign.FirstOrDefaultAsync(c => c.Id == @event.CampaignId);
            if (campaign != null)
            {
                ViewBag.CampaignName = campaign.Name;
            }

            return View(@event);
        }

        // GET: Events/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Event.FirstOrDefaultAsync(m => m.Id == id);
            if (@event == null)
            {
                return NotFound();
            }

            var campaign = await _context.Campaign.FirstOrDefaultAsync(c => c.Id == @event.CampaignId);
            if (campaign != null)
            {
                ViewBag.CampaignName = campaign.Name;
            }

            return View(@event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @event = await _context.Event.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }

            var campaign = _context.Campaign.FirstOrDefault(c => c.Id == @event.CampaignId);
            if (campaign != null)
            {
                _context.Event.Remove(@event);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Events", new { campaignId = @event.CampaignId, campaignName = campaign.Name });
            }

            return RedirectToAction("Index");
        }



        private int GetRandomStatValue()
        {
            // Implementacja losowego generowania wartości statystyki od 1 do 20
            Random random = new Random();
            return random.Next(1, 21);
        }
    }
}

﻿using System;
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
    public class CampaignsController : Controller
    {
        private readonly dndhelperContext _context;

        public CampaignsController(dndhelperContext context)
        {
            _context = context;
        }

        // GET: Campaigns
        public IActionResult Index()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var campaigns = _context.Campaign.Where(c => c.UserId == userId).ToList();
            return View(campaigns);
        }
        // Metoda sprawdzająca czy nazwa kampanii się nie powtarza
        [AcceptVerbs("GET", "POST")]
        public IActionResult IsCampaignNameUnique(string name)
        {
            var isUnique = !_context.Campaign.Any(c => c.Name == name);
            return Json(isUnique);
        }

        // GET: Campaigns/Create
        public IActionResult Create(int campaignId)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            var campaign = new Campaign
            {
                UserId = userId,
                CampaignId = campaignId
            };

            return View(campaign);
        }

        // POST: Campaigns/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name,UserId,CampaignId")] Campaign campaign)
        {
            if (ModelState.IsValid)
            {
                campaign.UserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                _context.Add(campaign);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(campaign);
        }



        // GET: Campaigns/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var campaign = await _context.Campaign.FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);

            if (campaign == null)
            {
                return NotFound();
            }

            return View(campaign);
        }

        // GET: Campaigns/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var campaign = await _context.Campaign.FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);

            if (campaign == null)
            {
                return NotFound();
            }

            return View(campaign);
        }

        // POST: Campaigns/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,UserId")] Campaign campaign)
        {
            if (id != campaign.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(campaign);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CampaignExists(campaign.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (Exception e)
                {
                    // Obsługa błędów
                    // Miałem problem z UserId i mimo rozwiązania błędu, stwierdziłem że lepiej zostawić
                    ModelState.AddModelError("", "Wystąpił błąd podczas edycji kampanii: " + e.Message);
                }
            }
            return View(campaign);
        }

        // GET: Campaigns/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var campaign = await _context.Campaign.FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);

            if (campaign == null)
            {
                return NotFound();
            }

            return View(campaign);
        }

        // POST: Campaigns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var campaign = await _context.Campaign.FindAsync(id);
            _context.Campaign.Remove(campaign);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CampaignExists(int id)
        {
            return _context.Campaign.Any(c => c.Id == id);
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Data;
using Data.Models;

namespace ConsoleApp.Web.Controllers
{
    public class RoomsController : Controller
    {
        private readonly GeneralContext _context;

        public RoomsController(GeneralContext context)
        {
            _context = context;
        }

        // GET: Rooms
        public async Task<IActionResult> Index()
        {
            var generalContext = _context.Rooms.Include(r => r.Floor).Include(r => r.Laboratory).Include(r => r.Position).Include(r => r.RType);
            return View(await generalContext.ToListAsync());
        }

        // GET: Rooms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _context.Rooms
                .Include(r => r.Floor)
                .Include(r => r.Laboratory)
                .Include(r => r.Position)
                .Include(r => r.RType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        // GET: Rooms/Create
        public IActionResult Create()
        {
            ViewData["FloorId"] = new SelectList(_context.Floors, "Id", "Number");
            ViewData["LaboratoryId"] = new SelectList(_context.Laboratories, "Id", "Name");
            ViewData["PositionId"] = new SelectList(_context.Positions, "Id", "Name");
            ViewData["RTypeId"] = new SelectList(_context.RTypes, "Id", "Name");
            return View();
        }

        // POST: Rooms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Number,FloorId,Length,Width,PositionId,RTypeId,ForWhat,LaboratoryId")] Room room)
        {
            if (ModelState.IsValid)
            {
                _context.Add(room);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FloorId"] = new SelectList(_context.Floors, "Id", "Id", room.FloorId);
            ViewData["LaboratoryId"] = new SelectList(_context.Laboratories, "Id", "Id", room.LaboratoryId);
            ViewData["PositionId"] = new SelectList(_context.Positions, "Id", "Id", room.PositionId);
            ViewData["RTypeId"] = new SelectList(_context.RTypes, "Id", "Id", room.RTypeId);
            return View(room);
        }

        // GET: Rooms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _context.Rooms.FindAsync(id);
            if (room == null)
            {
                return NotFound();
            }
            ViewData["FloorId"] = new SelectList(_context.Floors, "Id", "Id", room.FloorId);
            ViewData["LaboratoryId"] = new SelectList(_context.Laboratories, "Id", "Id", room.LaboratoryId);
            ViewData["PositionId"] = new SelectList(_context.Positions, "Id", "Id", room.PositionId);
            ViewData["RTypeId"] = new SelectList(_context.RTypes, "Id", "Id", room.RTypeId);
            return View(room);
        }

        // POST: Rooms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Number,FloorId,Length,Width,PositionId,RTypeId,ForWhat,LaboratoryId")] Room room)
        {
            if (id != room.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(room);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoomExists(room.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["FloorId"] = new SelectList(_context.Floors, "Id", "Id", room.FloorId);
            ViewData["LaboratoryId"] = new SelectList(_context.Laboratories, "Id", "Id", room.LaboratoryId);
            ViewData["PositionId"] = new SelectList(_context.Positions, "Id", "Id", room.PositionId);
            ViewData["RTypeId"] = new SelectList(_context.RTypes, "Id", "Id", room.RTypeId);
            return View(room);
        }

        // GET: Rooms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _context.Rooms
                .Include(r => r.Floor)
                .Include(r => r.Laboratory)
                .Include(r => r.Position)
                .Include(r => r.RType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        // POST: Rooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoomExists(int id)
        {
            return _context.Rooms.Any(e => e.Id == id);
        }
    }
}

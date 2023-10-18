﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserService.Models;

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessProfilesController : ControllerBase
    {
        private readonly userServiceContext _context;

        public BusinessProfilesController(userServiceContext context)
        {
            _context = context;
        }

        // GET: api/BusinessProfiles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BusinessProfile>>> GetBusinessProfiles()
        {
            return await _context.BusinessProfiles.ToListAsync();
        }

        // GET: api/BusinessProfiles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BusinessProfile>> GetBusinessProfile(int id)
        {
            var businessProfile = await _context.BusinessProfiles.FindAsync(id);

            if (businessProfile == null)
            {
                return NotFound();
            }

            return businessProfile;
        }

        // PUT: api/BusinessProfiles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBusinessProfile(int id, BusinessProfile businessProfile)
        {
            if (id != businessProfile.BusinessId)
            {
                return BadRequest();
            }

            _context.Entry(businessProfile).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BusinessProfileExists(id))
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

        // POST: api/BusinessProfiles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BusinessProfile>> PostBusinessProfile(BusinessProfile businessProfile)
        {
            _context.BusinessProfiles.Add(businessProfile);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBusinessProfile", new { id = businessProfile.BusinessId }, businessProfile);
        }

        // DELETE: api/BusinessProfiles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBusinessProfile(int id)
        {
            var businessProfile = await _context.BusinessProfiles.FindAsync(id);
            if (businessProfile == null)
            {
                return NotFound();
            }

            _context.BusinessProfiles.Remove(businessProfile);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BusinessProfileExists(int id)
        {
            return _context.BusinessProfiles.Any(e => e.BusinessId == id);
        }
    }
}
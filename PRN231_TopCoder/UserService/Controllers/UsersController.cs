using System;
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
    public class UsersController : ControllerBase
    {
        private readonly userServiceContext _context;

        public UsersController(userServiceContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.Include(m => m.BusinessProfile).ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.Include(m => m.BusinessProfile)
                .Where(m => m.UserId == id)
                .FirstOrDefaultAsync(m => m.UserId == id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }


        // GET: api/Users/
        [HttpPost("{id}")]
        public async Task<IActionResult> Login([FromBody] UserLoginModel loginModel)
        {
            if (loginModel == null)
            {
                return BadRequest("Invalid login data.");
            }

            // Find the user by email
            var user = await _context.Users.Include(b => b.BusinessProfile)
                .FirstOrDefaultAsync(u => u.Email == loginModel.Email);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            var loginPass = BCrypt.Net.BCrypt.HashPassword(loginModel.Password);
            // Verify the password (you should use a secure password hashing mechanism)
            if (BCrypt.Net.BCrypt.Verify(loginPass, user.Password))
            {
                return Unauthorized("Invalid password.");
            }

            return Ok(user);
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.UserId)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;
            if (user.UserType == "Employer")
            {
                user.BusinessProfile.IsDelete = 1;
                _context.Entry(user.BusinessProfile).State = EntityState.Modified;
            }            

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("register")]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            var userList = await _context.Users.Include(m => m.BusinessProfile).ToListAsync();
            foreach (var item in userList)
            {
                if (user.Email == item.Email)
                {
                    return BadRequest("There is already have an account with this email");
                }
            }            
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.UserId }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Livestream_Backend_application.Models;
using Microsoft.AspNetCore.Authorization;
using Livestream_Backend_application.DataTransfer;

namespace Livestream_Backend_application.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly LivestreamDBContext _context;

        public UsersController(LivestreamDBContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            return await _context.appUsers.ToListAsync();
        }
        
        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetUsers(string id)
        {
            var users = await _context.appUsers.FindAsync(id);

            if (users != null)
            {
              return  users;
            }

                return BadRequest("Something went wrong!");
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("update/{id}")]
        public async Task<ActionResult<AppUser>> PutUsers(string id, AppUser users)
        {
            var foundUser = await _context.appUsers.FirstOrDefaultAsync(u => u.Id == id);
            if (foundUser == null)
            {
               return BadRequest("No user found with given id");
            }
            foundUser.FirstName = users.FirstName;
            foundUser.Lastname = users.Lastname;
            foundUser.Email = users.Email;
            foundUser.UserName = users.UserName;

            _context.Update(foundUser);
           await  _context.SaveChangesAsync();

            return foundUser;
        }

        // POST: api/Users
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<AppUser>> PostUsers(AppUser users)
        {
            _context.appUsers.Add(users);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsers", new { id = users.Id }, users);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AppUser>> DeleteUsers(string id)
        {
            var users = await _context.appUsers.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }

            _context.appUsers.Remove(users);
            await _context.SaveChangesAsync();

            return users;
        }

        private bool UsersExists(string id)
        {
            return _context.appUsers.Any(e => e.Id == id);
        }
    }
}

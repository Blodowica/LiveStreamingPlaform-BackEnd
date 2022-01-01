using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Livestream_Backend_application.Models;
using Livestream_Backend_application.Services.Interfaces;

namespace Livestream_Backend_application.Controller
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class StreamsController : ControllerBase
    {
        private readonly LivestreamDBContext _context;
        private readonly IUserStreamService _userStreamService;

        public StreamsController(LivestreamDBContext context, IUserStreamService userStreamService)
        {
            _context = context;
            _userStreamService = userStreamService;
        }

        // GET: api/Streams
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppStreams>>> GetStreams()
        {
            return await _context.Streams.ToListAsync();
        }

        // GET: api/Streams/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AppStreams>> GetStreams(int id)
        {
            var streams = await _context.Streams.FindAsync(id);

            if (streams == null)
            {
                return NotFound();
            }

            return streams;
        }

        // PUT: api/Streams/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<ActionResult<AppStreams>> PutStreams(int id, AppStreams streams)
        {
            try
            {
            var foundStream = await _context.Streams.FirstOrDefaultAsync(u => u.StreamId == id);
            if (foundStream == null)
            {
                return BadRequest("No user found with given id");
            }
                foundStream.StreamId = streams.StreamId;
                foundStream.Title = streams.Title;
                foundStream.Description = streams.Description ;
                foundStream.UserId = streams.UserId;

            _context.Update(foundStream);
            await _context.SaveChangesAsync();

            return foundStream;

            }
            catch (Exception ex)
            {

                return BadRequest(new { message = ex.Message });
            }
          

        }

        // POST: api/Streams
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<AppStreams>> PostStreams(AppStreams streams)
        {
            _context.Streams.Add(streams);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStreams", new { id = streams.StreamId }, streams);
        }

        // DELETE: api/Streams/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AppStreams>> DeleteStreams(int id)
        {
            var streams = await _context.Streams.FindAsync(id);
            if (streams == null)
            {
                return NotFound();
            }

            _context.Streams.Remove(streams);
            await _context.SaveChangesAsync();

            return streams;
        }
        [HttpGet("user-stream/{UserId}")]
        public async Task<ActionResult> GetUserStream(string UserId)
        {
            try
            {
                var stream = await _userStreamService.getUserStreamResponse(UserId);
                return Ok(stream);

            }
            catch (Exception ex )
            {

                return BadRequest(new { message = ex.Message });
            }
        
        }
        private bool StreamsExists(int id)
        {
            return _context.Streams.Any(e => e.StreamId == id);
        }
    }
}

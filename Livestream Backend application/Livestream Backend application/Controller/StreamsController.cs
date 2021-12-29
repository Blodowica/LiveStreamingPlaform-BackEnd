using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Livestream_Backend_application.Models;

namespace Livestream_Backend_application.Controller
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class StreamsController : ControllerBase
    {
        private readonly LivestreamDBContext _context;

        public StreamsController(LivestreamDBContext context)
        {
            _context = context;
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
        public async Task<IActionResult> PutStreams(int id, Streams streams)
        {
            if (id != streams.  StreamId)
            {
                return BadRequest();
            }

            _context.Entry(streams).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StreamsExists(id))
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

        private bool StreamsExists(int id)
        {
            return _context.Streams.Any(e => e.StreamId == id);
        }
    }
}

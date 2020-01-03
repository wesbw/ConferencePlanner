using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackEnd.Data;
using ConferenceDTO;
using System.Diagnostics;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public SessionController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: api/Session
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SessionResponse>>> GetSessions()
        {
            var sessions = await _db.Sessions.AsNoTracking()
            .Include(s=>s.Track)
            .Include(s=>s.SessionSpeakers)
            .ThenInclude(ss=>ss.Speaker)
            .Include(s=>s.SessionTags)
            .ThenInclude(ss=>ss.Tag)
            .Select(m=>m.MapSessionResponse())
            .ToListAsync();
            return sessions;
        }

        // GET: api/Session/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SessionResponse>> GetSession(int id)
        {
            var session = await _db.Sessions.AsNoTracking()
            .Include(s=>s.Track)
            .Include(s=>s.SessionSpeakers)
            .ThenInclude(ss=>ss.Speaker)
            .Include(s=>s.SessionTags)
            .ThenInclude(ss=>ss.Tag)
            .SingleOrDefaultAsync(s=>s.ID == id);
            if(session == null)
            {
                return NotFound();
            }
            return session.MapSessionResponse();
        }

        // PUT: api/Session/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSession(int id, ConferenceDTO.Session input)
        {
            
            var session = await _db.Sessions.FindAsync(id);
            if(session == null)
                return BadRequest();
            
           session.ID = input.ID;
           session.Title = input.Title;
           session.Abstract = input.Abstract;
           session.StartTime = input.StartTime;
           session.EndTime = input.EndTime;
           session.TrackId = input.TrackId;
           session.ConferenceID = input.ConferenceID;
           bool res = await _db.SaveChangesAsync() > 0;
           if(!res)
                Debugger.Break();
           return NoContent();
        }

        // POST: api/Session
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<SessionResponse>> PostSession(ConferenceDTO.Session input)
        {
            var session = new Data.Session{
                Title = input.Title,
                ConferenceID = input.ConferenceID,
                StartTime = input.StartTime,
                EndTime = input.EndTime,
                Abstract = input.Abstract,
                TrackId = input.TrackId
            };
            _db.Sessions.Add(session);
            
            bool res = await _db.SaveChangesAsync()>0;
            if(!res)
                Debugger.Break();
            var result = session.MapSessionResponse();
            return CreatedAtAction(nameof(GetSession), new {id = result.ID},result);
        }

        // DELETE: api/Session/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SessionResponse>> DeleteSession(int id)
        {
            var session = await _db.Sessions.FindAsync(id);
            if(session == null)
            return NotFound();
            _db.Sessions.Remove(session);
            await _db.SaveChangesAsync();
            return session.MapSessionResponse();
        }

    }
}

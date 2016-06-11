using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Filters;
using Netmedia.Common.Extensions;
using Netmedia.DumpDay.Data;
using Netmedia.DumpDay.Models;

namespace Netmedia.DumpDay.Controllers
{
    [System.Web.Http.Cors.EnableCors(origins: "*", headers: "*", methods: "*")]
    public class SessionsController : ApiController
    {
        private ModelContext db = new ModelContext();

        [HttpGet]
        public IQueryable<Session> GetAll(string search = null)
        {
            IQueryable<Session> sessions = db.Sessions;

            if (search.IsNotNullOrEmpty())
                sessions = sessions.Where(s => s.Title.Contains(search));

            return sessions;
        }

        [ResponseType(typeof(Session)), HttpGet]
        public IHttpActionResult Get(int id)
        {
            var session = db.Sessions.Find(id);
            if (session == null)
                return NotFound();

            return Ok(session);
        }

        
        [ResponseType(typeof(void)), HttpPost]
        public IHttpActionResult UpVote(int id)
        {
            return _ExecuteVotingAction(id, s => s.Votes += 1);
        }

        [ResponseType(typeof(void)), HttpPost]
        public IHttpActionResult DownVote(int id)
        {
            return _ExecuteVotingAction(id, s =>
            {
                if (s.Votes > 0) s.Votes -= 1;
            });
        }

        // POST: api/Sessions
        [ResponseType(typeof(Session)), HttpPost]
        public IHttpActionResult Create(Session session)
        {
            if (ModelState.IsValid == false) return BadRequest(ModelState);

            db.Sessions.Add(session);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = session.Id }, session);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) db.Dispose();

            base.Dispose(disposing);
        }

        private IHttpActionResult _ExecuteVotingAction(int id, Action<Session> sessionAction)
        {
            if (ModelState.IsValid == false) return BadRequest(ModelState);

            var session = db.Sessions.FirstOrDefault(s => s.Id == id);
            if (session == null) return BadRequest();

            sessionAction(session);

            db.Entry(session).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_SessionExists(id) == false)
                    return NotFound();

                throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        private bool _SessionExists(int id)
        {
            return db.Sessions.Count(e => e.Id == id) > 0;
        }
    }
}
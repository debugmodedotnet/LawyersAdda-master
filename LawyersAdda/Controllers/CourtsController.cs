using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using LawyersAdda.Entities;
using LawyersAdda.Models;

namespace LawyersAdda.Controllers
{
    public class CourtsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Courts
        public IQueryable<Court> GetCourts()
        {
            return db.Courts;
        }

        // GET: api/Courts/5
        [ResponseType(typeof(Court))]
        public async Task<IHttpActionResult> GetCourt(string id)
        {
            Court court = await db.Courts.FindAsync(id);
            if (court == null)
            {
                return NotFound();
            }

            return Ok(court);
        }

        // PUT: api/Courts/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCourt(string id, Court court)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != court.Id)
            {
                return BadRequest();
            }

            db.Entry(court).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourtExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Courts
        [ResponseType(typeof(Court))]
        public async Task<IHttpActionResult> PostCourt(Court court)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Courts.Add(court);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CourtExists(court.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = court.Id }, court);
        }

        // DELETE: api/Courts/5
        [ResponseType(typeof(Court))]
        public async Task<IHttpActionResult> DeleteCourt(string id)
        {
            Court court = await db.Courts.FindAsync(id);
            if (court == null)
            {
                return NotFound();
            }

            db.Courts.Remove(court);
            await db.SaveChangesAsync();

            return Ok(court);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CourtExists(string id)
        {
            return db.Courts.Count(e => e.Id == id) > 0;
        }
    }
}
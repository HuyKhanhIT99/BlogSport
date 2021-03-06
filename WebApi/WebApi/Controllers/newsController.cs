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
using WebApi.Models;

namespace WebApi.Controllers
{
    public class newsController : ApiController
    {
        private CRUBWEBTINTUCEntities db = new CRUBWEBTINTUCEntities();

        // GET: api/news
        public IQueryable<news> Getnews()
        {
            return db.news;
        }

        // GET: api/news/5
        [ResponseType(typeof(news))]
        public IHttpActionResult Getnews(long id)
        {
            news news = db.news.Find(id);
            if (news == null)
            {
                return NotFound();
            }

            return Ok(news);
        }

        // PUT: api/news/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putnews(long id, news news)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != news.ID)
            {
                return BadRequest();
            }

            db.Entry(news).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!newsExists(id))
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

        // POST: api/news
        [ResponseType(typeof(news))]
        public IHttpActionResult Postnews(news news)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.news.Add(news);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = news.ID }, news);
        }

        // DELETE: api/news/5
        [ResponseType(typeof(news))]
        public IHttpActionResult Deletenews(long id)
        {
            news news = db.news.Find(id);
            if (news == null)
            {
                return NotFound();
            }

            db.news.Remove(news);
            db.SaveChanges();

            return Ok(news);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool newsExists(long id)
        {
            return db.news.Count(e => e.ID == id) > 0;
        }
    }
}
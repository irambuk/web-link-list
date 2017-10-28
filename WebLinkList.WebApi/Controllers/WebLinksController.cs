using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebLinkList.EF;
using WebLinkList.EF.Model;

namespace WebLinkList.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class WebLinksController : Controller
    {
        private WebLinkContext _context;

        public WebLinksController(WebLinkContext context) {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<WebLink> Get()
        {
            return _context.WebLinks.ToList();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(WebLink), 200)]
        [ProducesResponseType(typeof(WebLink), 404)]
        public IActionResult Get(Guid id)
        {
            var webLink = _context.WebLinks.FirstOrDefault(w => w.Id == id);

            if (webLink == null)
            {
                return NotFound(id);
            }

            return Ok(webLink);
        }

        [HttpPost]
        [ProducesResponseType(typeof(WebLink), 200)]
        [ProducesResponseType(typeof(WebLink), 400)]
        [ProducesResponseType(typeof(WebLink), 422)]
        public IActionResult Post([FromBody]WebLink webLink)
        {
            if (webLink == null)
            {
                return BadRequest();
            }

            if (ModelState.IsValid) {
                if (webLink.Id == Guid.Empty)
                {
                    webLink.Id = new Guid();
                }

                _context.WebLinks.Add(webLink);
                _context.SaveChanges();

                Ok(webLink);
            }

            return StatusCode(422);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(WebLink), 200)]
        [ProducesResponseType(typeof(WebLink), 400)]
        [ProducesResponseType(typeof(WebLink), 404)]
        [ProducesResponseType(typeof(WebLink), 422)]
        public IActionResult Put(Guid id, [FromBody]WebLink webLink)
        {
            if (webLink == null)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var existingWebLink = _context.WebLinks.FirstOrDefault(w => w.Id == id);

                if (existingWebLink == null)
                {
                    return NotFound(id);
                }

                existingWebLink.Name = webLink.Name;
                existingWebLink.Url = webLink.Url;
                _context.SaveChanges();

                Ok(existingWebLink);
            }

            return StatusCode(422); 
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(WebLink), 200)]
        [ProducesResponseType(typeof(WebLink), 404)]
        public IActionResult Delete(Guid id)
        {
            var existingWebLink = _context.WebLinks.FirstOrDefault(w => w.Id == id);

            if (existingWebLink == null)
            {
                return NotFound(id);
            }

            _context.WebLinks.Remove(existingWebLink);
            _context.SaveChanges();

            return Ok();

        }
    }
}

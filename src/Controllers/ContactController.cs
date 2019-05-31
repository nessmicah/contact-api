using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ContactApi.interfaces;
using ContactApi.data.Models;

namespace ContactApi.Controllers
{
  [Produces("application/json"), Route("api/contacts")]
  [ApiController]
  public class ContactController : Controller
  {
    private IContactService _contentService;

    public ContactController(IContactService contentService)
    {
      _contentService = contentService;
    }

    // GET api/contacts
    [HttpGet]
    [ProducesResponseType(typeof(List<Contact>), 200)]
    [ProducesResponseType(404)]
    public IActionResult Get()
    {
      var contacts = _contentService.GetAll();

      if (contacts == null)
      {
          return NotFound();
      }

      return Json(contacts);
    }

    // GET api/contacts/5
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Contact), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public IActionResult Get(int id)
    {
      if (id <= 0)
      {
        return BadRequest("Please pass a valid id");
      }
      var contact = _contentService.GetById(id);

      if (contact != null)
      {
        return Json(contact);
      }

      return NotFound();
    }

    // POST api/contacts
    [HttpPost]
    [ProducesResponseType(typeof(Contact), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public IActionResult Post([FromBody] Contact contact)
    {
      //Assumes only basic info required for contact
      if (string.IsNullOrEmpty(contact.Name.First) || string.IsNullOrEmpty(contact.Name.Last))
      {
        return BadRequest("Please fill out all required fields");
      }
      var contactResult = _contentService.Add(contact);

      if (contactResult != null)
      {
        return Ok(contactResult);
      }

      return NotFound();

    }

    // PUT api/contacts/5
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(Contact), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public IActionResult Put(int id, [FromBody] Contact contact)
    {
      if (id <= 0)
      {
        return BadRequest("Please pass a valid id");
      }

      //Assumes only basic info required for contact
      if (string.IsNullOrEmpty(contact.Name.First) || string.IsNullOrEmpty(contact.Name.Last))
      {
        return BadRequest("Please fill out all required fields");
      }

      if (contact.Id <= 0)
      {
        contact.Id = id;
      }

      var contactResult = _contentService.Update(id, contact);

      if (contactResult != null)
      {
        return Ok(contactResult);
      }

      return NotFound();
    }

    // DELETE api/contacts/5
    [HttpDelete("{id}")]
    public bool Delete(int id)
    {
      var result = _contentService.Delete(id);

      return result;
    }
  }
}

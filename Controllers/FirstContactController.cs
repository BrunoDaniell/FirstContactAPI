using FirstContactAPI.Model;
using FirstContactAPI.Repository;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace FirstContactAPI.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    
    public class FirstContactController : ControllerBase
    {
        private readonly IFirstContactRepository _firstContactRepository;
        public FirstContactController(IFirstContactRepository firstContactRepository)
        {
            _firstContactRepository = firstContactRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<FirstContact>> GetFirstContacts()
        {
            return await _firstContactRepository.Get();
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<FirstContact>> GetFirstContacts(int id)
        {
            return await _firstContactRepository.Get(id);
        }

        [HttpPost]
        public async Task<ActionResult<object>> PostFirstContacts([FromForm] FirstContact firstContact)
        {
            FirstContact newContact = await _firstContactRepository.Create(firstContact);
            return CreatedAtAction(nameof(GetFirstContacts), new {id = newContact.Id}, newContact);
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var contactToDelete = await _firstContactRepository.Get(id);

            if (contactToDelete == null)
                return NotFound();

            await _firstContactRepository.Delete(contactToDelete.Id);
            return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult> PutFirstContacts(int id, [FromBody] FirstContact firstContact)
        {
            if (id != firstContact.Id)
                return BadRequest();

                await _firstContactRepository.Update(firstContact);
            return NoContent();
        }
    }
}

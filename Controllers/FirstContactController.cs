﻿using FirstContactAPI.Model;
using FirstContactAPI.Repository;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IEnumerable<object>> GetFirstContacts()
        {
            return await _firstContactRepository.Get();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFirstContacts([FromRoute] int id)
        {
            var document = await _firstContactRepository.Get(id);
            if (document is null)
                return NotFound($"Documento for Id: {id}");

            return File(document.FileContent, "application/pdf", document.FileName);
        }

        [HttpPost]
        public async Task<ActionResult<object>> PostFirstContacts([FromForm] CandidateData candidateData)
        {
            using var memoryStream = new MemoryStream();
            await candidateData.File.CopyToAsync(memoryStream);

            var firstContact = new FirstContact
            {
                Id = candidateData.Id,
                Name = candidateData.Name,
                FileName = candidateData.File.FileName,
                FileContent = memoryStream.ToArray()
            };

            var newContact = await _firstContactRepository.Create(firstContact);
            return CreatedAtAction(nameof(GetFirstContacts), new { id = newContact.Id }, newContact);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            var contactToDelete = await _firstContactRepository.Get(id);

            if (contactToDelete == null)
                return NotFound();

            await _firstContactRepository.Delete(contactToDelete.Id);
            return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult> PutFirstContacts(int id, [FromForm] CandidateData candidateData)
        {
            if (id != candidateData.Id)
                return BadRequest();

            using var memoryStream = new MemoryStream();
            await candidateData.File.CopyToAsync(memoryStream);

            var firstContact = new FirstContact
            {
                Id = candidateData.Id,
                Name = candidateData.Name,
                FileName = candidateData.File.FileName,
                FileContent = memoryStream.ToArray()
            };

            await _firstContactRepository.Update(firstContact);
            return NoContent();
        }
    }
}

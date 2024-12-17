using Microsoft.AspNetCore.Mvc;
using SimpleNotesApi.Models;
using System.Collections.Generic;
using System.Linq;
using SimpleNotesApi.Data;
using Microsoft.EntityFrameworkCore;


namespace SimpleNotesApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotesController : ControllerBase
    {
        private readonly NotesDbContext _context;

        public NotesController(NotesDbContext context)
        {
            _context = context;
        }

        // GET /api/notes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Note>>> GetNotes()
        {
            var notes = await _context.Notes.ToListAsync();
            return Ok(notes);
        }

        // GET /api/notes/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Note>> GetNoteById(int id)
        {
            var note = await _context.Notes.FindAsync(id);
            if (note == null) return NotFound();
            return Ok(note);
        }

        // POST /api/notes
        [HttpPost]
        public async Task<ActionResult<Note>> CreateNote([FromBody] Note newNote)
        {
            _context.Notes.Add(newNote);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetNoteById), new { id = newNote.Id }, newNote);
        }

        // PUT /api/notes/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateNote(int id, [FromBody] Note updatedNote)
        {
            var existingNote = await _context.Notes.FindAsync(id);
            if (existingNote == null) return NotFound();

            existingNote.Title = updatedNote.Title;
            existingNote.Content = updatedNote.Content;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE /api/notes/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteNote(int id)
        {
            var note = await _context.Notes.FindAsync(id);
            if (note == null) return NotFound();

            _context.Notes.Remove(note);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

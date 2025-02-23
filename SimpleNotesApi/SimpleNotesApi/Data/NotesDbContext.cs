// NotesDbContext.cs
using Microsoft.EntityFrameworkCore;
using SimpleNotesApi.Models; // Ensure you have this for the Note model

namespace SimpleNotesApi.Data
{
    public class NotesDbContext : DbContext
    {
        public NotesDbContext(DbContextOptions<NotesDbContext> options)
            : base(options)
        {
        }

        public DbSet<Note> Notes { get; set; }
    }
}

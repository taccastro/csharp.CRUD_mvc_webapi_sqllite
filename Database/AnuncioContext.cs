using AnuncioWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace AnuncioWeb.Database
{
    public class AnuncioContext : DbContext
    {

        public AnuncioContext(DbContextOptions<AnuncioContext>options) : base(options)
        {

        }

        public DbSet<Anuncio> Anuncios { get; set; }
    }
}

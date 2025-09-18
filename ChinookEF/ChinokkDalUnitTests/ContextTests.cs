using ChinookDal.Model;
using Microsoft.EntityFrameworkCore;

namespace ChinokkDalUnitTests;

public class ChinookContextTests
{
    string connection = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Chinook;Integrated Security=True;";

    private ChinookContext GetContext(bool logging)
    {
        DbContextOptionsBuilder<ChinookContext> optionsBuilder = new DbContextOptionsBuilder<ChinookContext>().UseSqlServer(connection);

        if (logging)
        {
            optionsBuilder.LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
        }

        return new ChinookContext(optionsBuilder.Options);
    }


    [Test]
    public void CanGetArtists()
    {
        using ChinookContext context = GetContext(logging: true);
        List<Artist> artists = context.Artists.ToList();
        // Replace this line:
        // Assert.AreEqual(275, artists.Count);

        // With this line:
        Assert.That(artists.Count, Is.EqualTo(275));
    }

    [Test]
    public void CanChangeArtist()
    {
        using ChinookContext context = GetContext(logging: true);
        Artist artist = context.Artists.First();
        artist.Name = "The Artist formerly known as AC/DC";
        context.SaveChanges();
    }

    [Test]
    public void CanAddAlbum()
    {
        using ChinookContext context = GetContext(logging: true);

        Artist artist = context.Artists.First();
        Genre genre = context.Genres.First();

        if (artist != null && genre != null)
        {
            Album album = new Album() { Title = "The new one", Artist = artist };

            for (int i = 0; i < 10; i++)
            {
                Track track = new Track()
                {
                    Name = $"Opus {i}",
                    Album = album,
                    Genre = genre,
                    MediaTypeId = 1
                };

                album.Tracks.Add(track);
            }

            context.Albums.Add(album);
            context.SaveChanges();

            Assert.Pass();
        }
        else
            Assert.Fail();
    }
}
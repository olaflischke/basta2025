using ChickenEfDal.Model;
using Microsoft.EntityFrameworkCore;

namespace ChickenEfDalUnitTests;

public class ChickenContextTests
{
    [Test]
    public void CanGetChickensWithEggs()
    {
        ChickenContext context = GetContext();

        List<Chicken> qChickens = context.Chickens.Include(ck => ck.Eggs).ToList();

        Assert.AreEqual(3, qChickens.Count());
    }


    [Test]
    public void CanCreateChickenWithEggs()
    {
        ChickenContext context = GetContext();

        Chicken hedwig = new Chicken() { Name = "Hedwig", Weight = 2661 };

        List<Egg> eggs = new List<Egg>();
        for (int i = 0; i < Random.Shared.Next(5); i++)
        {
            eggs.Add(new Egg() { Weight = Random.Shared.Next(45, 81), Color = Random.Shared.Next(2) });
        }

        hedwig.Eggs = eggs;

        context.Chickens.Add(hedwig);

        context.SaveChanges();

        Assert.Pass();
    }


    private ChickenContext GetContext()
    {
        string connection = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ChickenDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

        DbContextOptionsBuilder<ChickenContext> builder = new DbContextOptionsBuilder<ChickenContext>().UseSqlServer(connection).LogTo(log => Console.WriteLine(log), Microsoft.Extensions.Logging.LogLevel.Information);

        return new ChickenContext(builder.Options);
    }


}
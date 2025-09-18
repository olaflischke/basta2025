using ChickenNhDalNoAutoPks;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Linq;
using System.Data;

namespace ChickenNhDalNoAutoPksUnitTests;

public class Tests
{

    [Test]
    public void CanGetChickenWithEggs()
    {
        Configuration configuration = ConfigureNHibernate();
        ISessionFactory factory = configuration.BuildSessionFactory();

        using (ISession session = factory.OpenSession())
        {
            List<Chicken> q = session.Query<Chicken>().Fetch(ck => ck.Eggs).ToList();

            Assert.AreEqual(3, q.Count);
        }
    }

    [Test]
    public void CanCreateChickenWithEggs()
    {
        Configuration configuration = ConfigureNHibernate();
        ISessionFactory factory = configuration.BuildSessionFactory();

        using (ISession session = factory.OpenSession())
        {
            Chicken hedwig = new Chicken() { Name = "Hedwig", Weight = 2661 };

            List<Egg> eggs = new List<Egg>();
            for (int i = 0; i < Random.Shared.Next(5); i++)
            {
                eggs.Add(new Egg() { Weight = Random.Shared.Next(45, 81), Color = Random.Shared.Next(2) });
            }

            hedwig.Eggs = eggs;

            session.Save(hedwig);

            session.Flush();
        }
    }


    public static Configuration ConfigureNHibernate()
    {
        var cfg = new Configuration();

        cfg.DataBaseIntegration(x =>
        {
            x.ConnectionString = "Server=(localdb)\\mssqllocaldb;Database=ChickenDbNoAutoPks;Trusted_Connection=True;";
            x.Driver<MicrosoftDataSqlClientDriver>();
            x.Dialect<MsSql2012Dialect>(); // TODO: Dynamisch ermittelbar?
            x.IsolationLevel = IsolationLevel.RepeatableRead;
            x.LogSqlInConsole = true;
            x.Timeout = 10;
            x.BatchSize = 10;
        });

        cfg.SessionFactory().GenerateStatistics();

        cfg.AddAssembly("ChickenNhDalNoAutoPks"); // Referenz zur Assembly mit den POCOs und dem Mapping

        return cfg;
    }

}

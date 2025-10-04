using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Trophies;

namespace Trophies.Tests;

[TestClass]
public class TrophiesRepositoryTests
{
   
    private static TrophiesRepository NewRepo() => new();

 
    [TestMethod]
    public void Get_Returns_Copies_And_Supports_Filter_And_Sort()
    {
        var repo = NewRepo();

        var all = repo.Get().ToList();
        Assert.IsTrue(all.Count >= 5);

        var firstCopy = all[0];
        var original = repo.GetById(firstCopy.Id)!;
        firstCopy.Competition = "XXX";
        firstCopy.Year = 1970;
        Assert.AreNotEqual(firstCopy.Competition, original.Competition);
        Assert.AreNotEqual(firstCopy.Year, original.Year);

        var y = all[0].Year;
        var onlyY = repo.Get(year: y).ToList();
        Assert.IsTrue(onlyY.All(t => t.Year == y));

      
        var byCompAsc = repo.Get(sortBy: "Competition").ToList();
        var sortedAsc = byCompAsc.OrderBy(t => t.Competition).ToList();
        CollectionAssert.AreEqual(sortedAsc, byCompAsc, "Skal være sorteret stigende på Competition.");

   
        var byYearDesc = repo.Get(sortBy: "Year", descending: true).ToList();
        var sortedDesc = byYearDesc.OrderByDescending(t => t.Year).ToList();
        CollectionAssert.AreEqual(sortedDesc, byYearDesc, "Skal være sorteret faldende på Year.");
    }

 
    [TestMethod]
    public void Add_Assigns_Id_And_Makes_Item_Retrievable()
    {
        var repo = NewRepo();

        var added = repo.Add(new Trophy(0, "Spring Cup", 2024));
        Assert.IsTrue(added.Id > 0);
        Assert.AreEqual("Spring Cup", added.Competition);
        Assert.AreEqual(2024, added.Year);

        var fetched = repo.GetById(added.Id);
        Assert.IsNotNull(fetched);
        Assert.AreEqual("Spring Cup", fetched!.Competition);
        Assert.AreEqual(2024, fetched.Year);
    }

    [TestMethod]
    public void Update_Changes_Competition_And_Year_But_Not_Id()
    {
        var repo = new TrophiesRepository();

        var any = repo.Get().First();
        var id = any.Id;

        var updated = repo.Update(id, new Trophy(0, "Updated Name", 2025));
        Assert.IsNotNull(updated);
        Assert.AreEqual(id, updated!.Id);
        Assert.AreEqual("Updated Name", updated.Competition);
        Assert.AreEqual(2025, updated.Year);

        var missing = repo.Update(-123, new Trophy(0, "TestCup", 2020));
        Assert.IsNull(missing);
    }

}

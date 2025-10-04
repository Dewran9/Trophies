using Microsoft.VisualStudio.TestTools.UnitTesting;
using Trophies;

namespace Trophies.Tests;

[TestClass]
public class TrophyTests
{
    [TestMethod]
    public void Ctor_Valid_Sets_All()
    {
        var t = new Trophy(1, "Super Cup", 2020);
        Assert.AreEqual(1, t.Id);
        Assert.AreEqual("Super Cup", t.Competition);
        Assert.AreEqual(2020, t.Year);
        Assert.AreEqual("1: Super Cup (2020)", t.ToString());
    }

    
    [TestMethod]
    public void Competition_Null_Throws_ArgumentNullException()
    {
        var t = new Trophy();
        Assert.ThrowsException<ArgumentNullException>(() => t.Competition = null!);
    }

   
    [DataTestMethod]
    [DataRow("")]
    [DataRow("  ")]
    [DataRow("AB")]  
    public void Competition_Invalid_Throws_ArgumentException(string input)
    {
        var t = new Trophy();
        Assert.ThrowsException<ArgumentException>(() => t.Competition = input);
    }


    [TestMethod]
    public void Competition_Trimmed_And_Valid()
    {
        var t = new Trophy();
        t.Competition = "  Champions  ";
        Assert.AreEqual("Champions", t.Competition);
    }

    [DataTestMethod]
    [DataRow(1969)]
    [DataRow(2026)]
    public void Year_Invalid_Throws(int year)
    {
        var t = new Trophy();
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => t.Year = year);
    }

    [DataTestMethod]
    [DataRow(1970)]
    [DataRow(2000)]
    [DataRow(2025)]
    public void Year_Valid_Works(int year)
    {
        var t = new Trophy();
        t.Year = year;
        Assert.AreEqual(year, t.Year);
    }
}

namespace test;

using ChDb;

[TestClass]
public class ChDbTest
{
    [TestMethod]
    public void QueryVersionTest()
    {
        var result = ChDb.Query("select version()");
        Assert.IsNotNull(result);
        Assert.AreEqual(1UL, result.RowsRead);
        Assert.AreEqual(52UL, result.BytesRead);
        Assert.AreEqual("23.10.1.1\n", result.Buf);
        Assert.IsNull(result.ErrorMessage);
        Assert.AreNotEqual(TimeSpan.Zero, result.Elapsed);
        Assert.IsTrue(0.1 > result.Elapsed.TotalSeconds);
    }

    [TestMethod]
    public void QueryNullTest()
    {
        Assert.ThrowsException<ArgumentNullException>(() => ChDb.Query(null!));
        Assert.ThrowsException<ArgumentException>(() => ChDb.Query("wrong_query"));
        Assert.ThrowsException<ArgumentException>(() => ChDb.Query("wrong_query", "PrettyCompact"));
        Assert.ThrowsException<ArgumentException>(() => ChDb.Query("select version()", "wrong_format"));
    }

    [TestMethod]
    public void EmptyResultTest()
    {
        var result = ChDb.Query("show tables");
        Assert.IsNotNull(result);
        Assert.AreEqual(0UL, result.RowsRead);
        Assert.AreEqual(0UL, result.BytesRead);
        Assert.AreEqual("", result.Buf);
        Assert.IsNull(result.ErrorMessage);
        Assert.AreNotEqual(TimeSpan.Zero, result.Elapsed);
        Assert.IsTrue(0.1 > result.Elapsed.TotalSeconds);
    }

    [TestMethod]
    public void RowNumberTest()
    {
        var result = ChDb.Query("SELECT * FROM numbers(10)");
        Assert.IsNotNull(result);
        Assert.AreEqual(10UL, result.RowsRead);
    }

    [TestMethod]
    public void FormatTest()
    {
        Assert.AreEqual("1\t2\t3\n", ChDb.Query("SELECT 1 as a, 2 b, 3 c")!.Buf);
        Assert.AreEqual("1,2,3\n", ChDb.Query("SELECT 1 as a, 2 b, 3 c", "CSV")!.Buf);
        Assert.AreEqual("\"a\",\"b\",\"c\"\n1,2,3\n", ChDb.Query("SELECT 1 as a, 2 b, 3 c", "CSVWithNames")!.Buf);
        StringAssert.Contains(ChDb.Query("SELECT 1 as a, 2 b, 3 c", "CSVWithNamesAndTypes")!.Buf, "UInt8");
    }

    [TestMethod]
    public void InMemoryTest()
    {
        var sql =
            "create table test (a UInt8, b UInt8, c UInt8) Engine=Memory;" +
            "insert into test values (1, 2, 3);" +
            "select * from test; show tables;" +
            "drop table test;show tables";
        var result = ChDb.Query(sql);
        Assert.AreEqual("", result.Buf);
        Assert.AreEqual(null, result.ErrorMessage);
    }

    [TestMethod]
    [Ignore("Work in progress")]
    public void SessionTest()
    {
        var session = new Session
        {
            Format = "CSVWithNamesAndTypes",
            DataPath = "/var/lib/clickhouse/",
            UdfPath = "/usr/lib/clickhouse/udf/",
            LogLevel = "trace",
        };
        var result = session.Execute("select 1");
        Assert.AreEqual("1\n", result!.Buf);
    }
}
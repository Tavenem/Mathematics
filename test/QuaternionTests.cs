using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tavenem.Mathematics.Test;

[TestClass]
public class QuaternionTests
{
    [TestMethod]
    public void SerializationTest()
    {
        var q = new Quaternion<double>(-0.11, 0.15, 0.42, 0.13);

        var json = JsonSerializer.Serialize(q);
        Console.WriteLine(json);

        var deserialized = JsonSerializer.Deserialize<Quaternion<double>>(json);
        Assert.AreEqual(q, deserialized);
    }
}

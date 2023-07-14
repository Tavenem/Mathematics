using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tavenem.Mathematics.Test.Numerics;

[TestClass]
public class PlaneTests
{
    [TestMethod]
    public void SerializationTest()
    {
        var v = new Plane<double>(-14e33, 16e19, 45e18, -1e33);

        var json = JsonSerializer.Serialize(v);
        Console.WriteLine(json);

        var deserialized = JsonSerializer.Deserialize<Plane<double>>(json);
        Assert.AreEqual(v, deserialized);

        json = JsonSerializer.Serialize(v, MathematicsSourceGenerationContext.Default.PlaneDouble);
        Console.WriteLine(json);

        deserialized = JsonSerializer.Deserialize(json, MathematicsSourceGenerationContext.Default.PlaneDouble);
        Assert.AreEqual(v, deserialized);
    }
}

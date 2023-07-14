using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tavenem.Mathematics.Test.Numerics;

[TestClass]
public class Vector3Tests
{
    [TestMethod]
    public void DistanceTest()
    {
        var v = new Vector3<double>(3695.22833059986211, 14240.886700416748, 10061301897653811e-12);
        var u = new Vector3<double>(543820545372633303e-12, 81932.5209795092131, 41566.8707279603732);
        _ = Vector3<double>.Distance(v, u);
    }

    [TestMethod]
    public void SerializationTest()
    {
        var v = new Vector3<double>(-14e33, 16e19, 45e18);

        var json = JsonSerializer.Serialize(v);
        Console.WriteLine(json);

        var deserialized = JsonSerializer.Deserialize<Vector3<double>>(json);
        Assert.AreEqual(v, deserialized);

        json = JsonSerializer.Serialize(v, MathematicsSourceGenerationContext.Default.Vector3Double);
        Console.WriteLine(json);

        deserialized = JsonSerializer.Deserialize(json, MathematicsSourceGenerationContext.Default.Vector3Double);
        Assert.AreEqual(v, deserialized);
    }
}

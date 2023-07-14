using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tavenem.Mathematics.Test.Numerics;

[TestClass]
public class Vector4Tests
{
    [TestMethod]
    public void SerializationTest()
    {
        var v = new Vector4<double>(-14e33, 16e19, 45e18, 5e16);

        var json = JsonSerializer.Serialize(v);
        Console.WriteLine(json);

        var deserialized = JsonSerializer.Deserialize<Vector4<double>>(json);
        Assert.AreEqual(v, deserialized);

        json = JsonSerializer.Serialize(v, MathematicsSourceGenerationContext.Default.Vector4Double);
        Console.WriteLine(json);

        deserialized = JsonSerializer.Deserialize(json, MathematicsSourceGenerationContext.Default.Vector4Double);
        Assert.AreEqual(v, deserialized);
    }
}

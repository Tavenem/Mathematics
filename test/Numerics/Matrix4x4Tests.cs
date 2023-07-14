using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tavenem.Mathematics.Test.Numerics;

[TestClass]
public class Matrix4x4Tests
{
    [TestMethod]
    public void SerializationTest()
    {
        var v = new Matrix4x4<double>(
            -14e33, 16e19, 45e18, -1e33,
            6e19, 4e18, 16e19, 45e18,
            6e19, 4e18, 16e19, 45e18,
            6e19, 4e18, 16e19, 45e18);

        var json = JsonSerializer.Serialize(v);
        Console.WriteLine(json);

        var deserialized = JsonSerializer.Deserialize<Matrix4x4<double>>(json);
        Assert.AreEqual(v, deserialized);

        json = JsonSerializer.Serialize(v, MathematicsSourceGenerationContext.Default.Matrix4x4Double);
        Console.WriteLine(json);

        deserialized = JsonSerializer.Deserialize(json, MathematicsSourceGenerationContext.Default.Matrix4x4Double);
        Assert.AreEqual(v, deserialized);
    }
}

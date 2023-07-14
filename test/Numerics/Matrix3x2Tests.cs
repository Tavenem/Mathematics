using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tavenem.Mathematics.Test.Numerics;

[TestClass]
public class Matrix3x2Tests
{
    [TestMethod]
    public void SerializationTest()
    {
        var v = new Matrix3x2<double>(-14e33, 16e19, 45e18, -1e33, 6e19, 4e18);

        var json = JsonSerializer.Serialize(v);
        Console.WriteLine(json);

        var deserialized = JsonSerializer.Deserialize<Matrix3x2<double>>(json);
        Assert.AreEqual(v, deserialized);

        json = JsonSerializer.Serialize(v, MathematicsSourceGenerationContext.Default.Matrix3x2Double);
        Console.WriteLine(json);

        deserialized = JsonSerializer.Deserialize(json, MathematicsSourceGenerationContext.Default.Matrix3x2Double);
        Assert.AreEqual(v, deserialized);
    }
}

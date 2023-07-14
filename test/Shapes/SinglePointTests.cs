using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tavenem.Mathematics.Test.Shapes;

[TestClass]
public class SinglePointTests
{
    [TestMethod]
    public void SerializationTest()
    {
        var position = new Vector3<double>(-14e33, 16e19, 45e18);
        var shape = new SinglePoint<double>(position);
        IShape<double> iShape = shape;

        var json = JsonSerializer.Serialize(shape);
        Console.WriteLine();
        Console.WriteLine(json);
        Assert.AreEqual(shape, JsonSerializer.Deserialize<SinglePoint<double>>(json));
        Assert.AreEqual(shape, JsonSerializer.Deserialize<IShape<double>>(json));

        json = JsonSerializer.Serialize(iShape);
        Console.WriteLine();
        Console.WriteLine(json);
        Assert.AreEqual(iShape, JsonSerializer.Deserialize<IShape<double>>(json));

        json = JsonSerializer.Serialize(shape, MathematicsSourceGenerationContext.Default.SinglePointDouble);
        Console.WriteLine();
        Console.WriteLine(json);
        Assert.AreEqual(
            shape,
            JsonSerializer.Deserialize(json, MathematicsSourceGenerationContext.Default.SinglePointDouble));
        Assert.AreEqual(
            shape,
            JsonSerializer.Deserialize(json, MathematicsSourceGenerationContext.Default.IShapeDouble));

        json = JsonSerializer.Serialize(iShape, MathematicsSourceGenerationContext.Default.IShapeDouble);
        Console.WriteLine();
        Console.WriteLine(json);
        Assert.AreEqual(
            iShape,
            JsonSerializer.Deserialize(json, MathematicsSourceGenerationContext.Default.IShapeDouble));
    }
}

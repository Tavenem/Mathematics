using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tavenem.Mathematics.Test;

[TestClass]
public class ShapeTests
{
    [TestMethod]
    public void SphereTest()
    {
        var radius = 10.0;
        var sphere = new Sphere<double>(radius);
        Assert.IsTrue(sphere.Volume.IsNearlyEqualTo(4188.79, 0.01));

        radius = 94607e21;
        var position = new Vector3<double>(-14e33, 16e19, 45e18);
        _ = new Sphere<double>(radius, position);
    }

    [TestMethod]
    public void SerializationTest()
    {
        const double minorRadius = 1234e20;
        const double majorRadius = 5678e22;
        var position = new Vector3<double>(-14e33, 16e19, 45e18);
        var rotation = new Quaternion<double>(-0.11, 0.15, 0.42, 0.13);
        var shape = new Torus<double>(majorRadius, minorRadius, position, rotation);

        var json = JsonSerializer.Serialize(shape);
        Console.WriteLine();
        Console.WriteLine(json);
        Assert.AreEqual(shape, JsonSerializer.Deserialize<Torus<double>>(json));
        Assert.AreEqual(shape, JsonSerializer.Deserialize<IShape<double>>(json));

        IShape<double> ishape = shape;

        json = JsonSerializer.Serialize(ishape);
        Console.WriteLine();
        Console.WriteLine(json);
        Assert.AreEqual(ishape, JsonSerializer.Deserialize<IShape<double>>(json));
    }
}

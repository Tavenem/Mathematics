using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Tavenem.HugeNumbers;
using Tavenem.Mathematics.HugeNumbers;

namespace Tavenem.Mathematics.Test.HugeNumbers
{
    [TestClass]
    public class ShapeTests
    {
        [TestMethod]
        public void SphereTest()
        {
            var radius = new HugeNumber(10);
            var sphere = new Sphere(radius);
            Assert.IsTrue(sphere.Volume.IsNearlyEqualTo(new HugeNumber(418879, -2), new HugeNumber(1, -2)));

            radius = new HugeNumber(94607, 21);
            var position = new Vector3(new HugeNumber(-14, 33), new HugeNumber(16, 19), new HugeNumber(45, 18));
            _ = new Sphere(radius, position);
        }

        [TestMethod]
        public void SerializationTest()
        {
            var minorRadius = new HugeNumber(1234, 20);
            var majorRadius = new HugeNumber(5678, 22);
            var position = new Vector3(new HugeNumber(-14, 33), new HugeNumber(16, 19), new HugeNumber(45, 18));
            var rotation = new Quaternion(new HugeNumber(-11, -2), new HugeNumber(15, -2), new HugeNumber(42, -2), new HugeNumber(13, -2));
            var shape = new Torus(majorRadius, minorRadius, position, rotation);

            var json = System.Text.Json.JsonSerializer.Serialize(shape);
            Console.WriteLine();
            Console.WriteLine(json);
            Assert.AreEqual(shape, System.Text.Json.JsonSerializer.Deserialize<Torus>(json));
            Assert.AreEqual(shape, System.Text.Json.JsonSerializer.Deserialize<IShape>(json));

            IShape ishape = shape;

            json = System.Text.Json.JsonSerializer.Serialize(ishape);
            Console.WriteLine();
            Console.WriteLine(json);
            Assert.AreEqual(ishape, System.Text.Json.JsonSerializer.Deserialize<IShape>(json));
        }
    }
}

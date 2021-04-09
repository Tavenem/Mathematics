using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Tavenem.HugeNumbers;
using Tavenem.Mathematics.HugeNumbers;

namespace Tavenem.Mathematics.Test.HugeNumbers
{
    [TestClass]
    public class Vector3Tests
    {
        [TestMethod]
        public void DistanceTest()
        {
            var v = new Vector3(new HugeNumber(369522833059986211, 4), new HugeNumber(14240886700416748, 5), new HugeNumber(10061301897653811, -12));
            var u = new Vector3(new HugeNumber(543820545372633303, -12), new HugeNumber(819325209795092131, 5), new HugeNumber(415668707279603732, 5));
            _ = v.Distance(u);
        }

        [TestMethod]
        public void SerializationTest()
        {
            var v = new Vector3(new HugeNumber(-14, 33), new HugeNumber(16, 19), new HugeNumber(45, 18));

            var json = System.Text.Json.JsonSerializer.Serialize(v);
            Console.WriteLine(json);

            var deserialized = System.Text.Json.JsonSerializer.Deserialize<Vector3>(json);
            Assert.AreEqual(v, deserialized);
        }
    }
}

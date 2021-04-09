using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Tavenem.HugeNumbers;
using Tavenem.Mathematics.HugeNumbers;

namespace Tavenem.Mathematics.Test.HugeNumbers
{
    [TestClass]
    public class QuaternionTests
    {
        [TestMethod]
        public void SerializationTest()
        {
            var q = new Quaternion(new HugeNumber(-11, -2), new HugeNumber(15, -2), new HugeNumber(42, -2), new HugeNumber(13, -2));

            var json = System.Text.Json.JsonSerializer.Serialize(q);
            Console.WriteLine(json);

            var deserialized = System.Text.Json.JsonSerializer.Deserialize<Quaternion>(json);
            Assert.AreEqual(q, deserialized);
        }
    }
}

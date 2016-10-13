using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZXing.Common
{
    /// <summary>
    /// <author>Sean Owen</author>
    /// </summary>
    [TestFixture]
    public sealed class BitSourceTestCase
    {
        [Test]
        public void testSource()
        {
            byte[] bytes = { 1, 2, 3, 4, 5 };
            BitSource source = new BitSource(bytes);
            Assert.AreEqual(40, source.available());
            Assert.AreEqual(0, source.readBits(1));
            Assert.AreEqual(39, source.available());
            Assert.AreEqual(0, source.readBits(6));
            Assert.AreEqual(33, source.available());
            Assert.AreEqual(1, source.readBits(1));
            Assert.AreEqual(32, source.available());
            Assert.AreEqual(2, source.readBits(8));
            Assert.AreEqual(24, source.available());
            Assert.AreEqual(12, source.readBits(10));
            Assert.AreEqual(14, source.available());
            Assert.AreEqual(16, source.readBits(8));
            Assert.AreEqual(6, source.available());
            Assert.AreEqual(5, source.readBits(6));
            Assert.AreEqual(0, source.available());
        }
    }
}

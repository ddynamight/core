using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZXing.Common
{
    [TestFixture]
    public sealed class StringUtilsTests
    {
        // tbd:
        //[Test]
        //public void testShortShiftJIS_1()
        //{
        //    // ÈáëÈ≠ö
        //    doTest(new byte[] { 0x8b, 0xe0, 0x8b, 0x9b, }, "SJIS");
        //}

        // tbd:
        //[Test]
        //public void testShortISO88591_1()
        //{
        //    // b√•d
        //    doTest(new byte[] { 0x62, 0xe5, 0x64, }, "ISO-8859-1");
        //}

        // tbd:
        //[Test]
        //public void testMixedShiftJIS_1()
        //{
        //    // Hello Èáë!
        //    doTest(new byte[]
        //              {
        //              0x48, 0x65, 0x6c, 0x6c, 0x6f,
        //              0x20, 0x8b, 0xe0, 0x21,
        //              },
        //           "SJIS");
        //}

        private static void doTest(byte[] bytes, String charsetName)
        {
            var charset = Encoding.GetEncoding(charsetName);
            String guessedName = StringUtils.guessEncoding(bytes, null);
            var guessedEncoding = Encoding.GetEncoding(guessedName);
            Assert.AreEqual(charset, guessedEncoding);
        }

        /**
         * Utility for printing out a string in given encoding as a Java statement, since it's better
         * to write that into the Java source file rather than risk character encoding issues in the 
         * source file itself
         */
        public static void main(String[] args)
        {
            var text = args[0];
            var charset = Encoding.GetEncoding(args[1]);
            var declaration = new StringBuilder();
            declaration.Append("new byte[] { ");
            foreach (byte b in charset.GetBytes(text)) {
                declaration.Append("(byte) 0x");
                declaration.Append((b & 0xFF).ToString("X"));
                declaration.Append(", ");
            }
            declaration.Append('}');
            Console.WriteLine(declaration);
        }
    }
}
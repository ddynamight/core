﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZXing.Common
{
    /// <summary>
    /// <author>Sean Owen</author>
    /// <author>dswitkin@google.com (Daniel Switkin)</author>
    /// </summary>
    [TestFixture]
    public sealed class BitMatrixTests
    {
        private static readonly int[] BIT_MATRIX_POINTS = { 1, 2, 2, 0, 3, 1 };

        [Test]
        public void testGetSet()
        {
            BitMatrix matrix = new BitMatrix(33);
            Assert.AreEqual(33, matrix.Height);
            for (int y = 0; y < 33; y++) {
                for (int x = 0; x < 33; x++) {
                    if (y * x % 3 == 0) {
                        matrix[x, y] = true;
                    }
                }
            }
            for (int y = 0; y < 33; y++) {
                for (int x = 0; x < 33; x++) {
                    Assert.AreEqual(y * x % 3 == 0, matrix[x, y]);
                }
            }
        }

        [Test]
        public void testSetRegion()
        {
            BitMatrix matrix = new BitMatrix(5);
            matrix.setRegion(1, 1, 3, 3);
            for (int y = 0; y < 5; y++) {
                for (int x = 0; x < 5; x++) {
                    Assert.AreEqual(y >= 1 && y <= 3 && x >= 1 && x <= 3, matrix[x, y]);
                }
            }
        }

        [Test]
        public void testRectangularMatrix()
        {
            BitMatrix matrix = new BitMatrix(75, 20);
            Assert.AreEqual(75, matrix.Width);
            Assert.AreEqual(20, matrix.Height);
            matrix[10, 0] = true;
            matrix[11, 1] = true;
            matrix[50, 2] = true;
            matrix[51, 3] = true;
            matrix.flip(74, 4);
            matrix.flip(0, 5);

            // Should all be on
            Assert.IsTrue(matrix[10, 0]);
            Assert.IsTrue(matrix[11, 1]);
            Assert.IsTrue(matrix[50, 2]);
            Assert.IsTrue(matrix[51, 3]);
            Assert.IsTrue(matrix[74, 4]);
            Assert.IsTrue(matrix[0, 5]);

            // Flip a couple back off
            matrix.flip(50, 2);
            matrix.flip(51, 3);
            Assert.IsFalse(matrix[50, 2]);
            Assert.IsFalse(matrix[51, 3]);
        }

        [Test]
        public void testRectangularSetRegion()
        {
            BitMatrix matrix = new BitMatrix(320, 240);
            Assert.AreEqual(320, matrix.Width);
            Assert.AreEqual(240, matrix.Height);
            matrix.setRegion(105, 22, 80, 12);

            // Only bits in the region should be on
            for (int y = 0; y < 240; y++) {
                for (int x = 0; x < 320; x++) {
                    Assert.AreEqual(y >= 22 && y < 34 && x >= 105 && x < 185, matrix[x, y]);
                }
            }
        }

        [Test]
        public void testGetRow()
        {
            BitMatrix matrix = new BitMatrix(102, 5);
            for (int x = 0; x < 102; x++) {
                if ((x & 0x03) == 0) {
                    matrix[x, 2] = true;
                }
            }

            // Should allocate
            BitArray array = matrix.getRow(2, null);
            Assert.AreEqual(102, array.Size);

            // Should reallocate
            BitArray array2 = new BitArray(60);
            array2 = matrix.getRow(2, array2);
            Assert.AreEqual(102, array2.Size);

            // Should use provided object, with original BitArray size
            BitArray array3 = new BitArray(200);
            array3 = matrix.getRow(2, array3);
            Assert.AreEqual(200, array3.Size);

            for (int x = 0; x < 102; x++) {
                bool on = (x & 0x03) == 0;
                Assert.AreEqual(on, array[x]);
                Assert.AreEqual(on, array2[x]);
                Assert.AreEqual(on, array3[x]);
            }
        }

        [Test]
        public void testRotate180Simple()
        {
            var matrix = new BitMatrix(3, 3);
            matrix[0, 0] = true;
            matrix[0, 1] = true;
            matrix[1, 2] = true;
            matrix[2, 1] = true;

            matrix.rotate180();

            Assert.IsTrue(matrix[2, 2]);
            Assert.IsTrue(matrix[2, 1]);
            Assert.IsTrue(matrix[1, 0]);
            Assert.IsTrue(matrix[0, 1]);
        }

        [Test]
        public void testRotate180()
        {
            testRotate180(7, 4);
            testRotate180(7, 5);
            testRotate180(8, 4);
            testRotate180(8, 5);
        }

        private static void testRotate180(int width, int height)
        {
            var input = getInput(width, height);
            input.rotate180();
            var expected = getExpected(width, height);

            for (int y = 0; y < height; y++) {
                for (int x = 0; x < width; x++) {
                    Assert.AreEqual(expected[x, y], input[x, y], "(" + x + ',' + y + ')');
                }
            }
        }

        private static BitMatrix getExpected(int width, int height)
        {
            var result = new BitMatrix(width, height);
            for (int i = 0; i < BIT_MATRIX_POINTS.Length; i += 2) {
                result[width - 1 - BIT_MATRIX_POINTS[i], height - 1 - BIT_MATRIX_POINTS[i + 1]] = true;
            }
            return result;
        }

        private static BitMatrix getInput(int width, int height)
        {
            var result = new BitMatrix(width, height);
            for (int i = 0; i < BIT_MATRIX_POINTS.Length; i += 2) {
                result[BIT_MATRIX_POINTS[i], BIT_MATRIX_POINTS[i + 1]] = true;
            }
            return result;
        }
    }
}
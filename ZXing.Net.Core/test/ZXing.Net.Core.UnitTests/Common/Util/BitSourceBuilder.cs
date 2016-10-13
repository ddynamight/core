using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ZXing.Common.Util
{
    /// <summary>
    /// Class that lets one easily build an array of bytes by appending bits at a time.
    ///
    /// <author>Sean Owen</author>
    /// </summary>
    public sealed class BitSourceBuilder
    {
        private MemoryStream output;
        private int nextByte;
        private int bitsLeftInNextByte;

        public BitSourceBuilder()
        {
            output = new MemoryStream();
            nextByte = 0;
            bitsLeftInNextByte = 8;
        }

        public void write(int value, int numBits)
        {
            if (numBits <= bitsLeftInNextByte) {
                nextByte <<= numBits;
                nextByte |= value;
                bitsLeftInNextByte -= numBits;
                if (bitsLeftInNextByte == 0) {
                    output.WriteByte((byte)nextByte);
                    nextByte = 0;
                    bitsLeftInNextByte = 8;
                }
            } else {
                int bitsToWriteNow = bitsLeftInNextByte;
                int numRestOfBits = numBits - bitsToWriteNow;
                int mask = 0xFF >> (8 - bitsToWriteNow);
                int valueToWriteNow = ((int)((uint)value >> numRestOfBits)) & mask;
                write(valueToWriteNow, bitsToWriteNow);
                write(value, numRestOfBits);
            }
        }

        public byte[] toByteArray()
        {
            if (bitsLeftInNextByte < 8) {
                write(0, bitsLeftInNextByte);
            }
            return output.ToArray();
        }
    }
}
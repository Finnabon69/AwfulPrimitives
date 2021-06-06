using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwfulPrimitives
{
    public class BadByte
    {
        private BadBit[] bits;

        private BadByte(byte value)
        {
            bits = new BadBit[8];
            for (int i = 0; i < 8; i++) bits[i] = new BadBit();
            State = value;
        }

        public byte State
        {
            get => GetFromBitArray(bits.Select(bb => bb.State).ToArray());
            set
            {
                var bits = GetBitArray(value);
                for (int bit = 0; bit < 8; bit++)
                {
                    this.bits[bit].State = bits[bit];
                }
            }
        }

        public static bool[] GetBitArray(byte value)
        {
            var outArray = new bool[8];
            for (int bit = 0; bit < 8; bit++)
            {
                outArray[bit] = (value & (1 << bit)) != 0;
            }
            return outArray;
        }

        public static byte GetFromBitArray(bool[] bits)
        {
            byte outByte = 0;
            for (int bit = 0; bit < 8; bit++)
            {
                outByte += (byte)((bits[bit] ? 1 : 0) << bit);
            }
            return outByte;
        }

        public static implicit operator BadByte(byte b) => new(b);
        public static implicit operator byte(BadByte b) => b.State;

    }
}

using Kyvos.Memory;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Veldrid;

namespace Kyvos.Core.Inputs
{
    [StructLayout( LayoutKind.Sequential, Pack = 1 )]
    internal struct InputState
    {
        int s0, s1, s2, s3;
        short s4;

        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        private (int, int) CalcOffsetAndIndex( int val )
        {
            var offset = val / (Size.Of<short>()*8);
            var idx = val - (offset * (Size.Of<short>()*8));
            return (offset, idx);
        }


        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        unsafe bool Get( int global_idx )
        {
            fixed (InputState* addr = &this)
            {
                short* s_addr = (short*)addr;

                (var offset, var idx) = CalcOffsetAndIndex( global_idx );

                return Convert.ToBoolean(
                    (*(s_addr + offset)) & (1 << idx)
                    );
            }
        }

        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        unsafe void Set( int global_idx, bool value )
        {
            var val = Convert.ToInt32(value);

            fixed (InputState* addr = &this)
            {
                short* s_addr = (short*)addr;

                (var offset, var idx) = CalcOffsetAndIndex( global_idx );

                if (value)
                {
                    *(s_addr + offset) |= (short)(1 << idx);

                } else
                {
                    *(s_addr + offset) &= (short)~(1 << idx);
                }
            }
        }

        public bool this[Key k]
        {
            get => Get( (int)k );

            internal set => Set( (int)k, value );
        }

        public bool this[MouseButton b]
        {
            get => Get( (int)Key.LastKey + (int)b );
            set => Set( (int)Key.LastKey + (int)b, value );
        }

        public void Clear()
        {
            s0 = s1 = s2 = s3 =
            s4 = 0;
        }

        public override string ToString()
        {
            return
                ToBitStringInt( s0 ) + "\n" +
                ToBitStringInt( s1 ) + "\n" +
                ToBitStringInt( s2 ) + "\n" +
                ToBitStringInt( s3 ) + "\n" +
                ToBitStringShort( s4 ) + "\n";

            string ToBitStringShort( short b )
            {
                StringBuilder builder = new();

                for (int i = 0; i < 16; i++)
                {
                    builder.Insert(0,((b % 2) == 0 ? "0" : "1"));
                    b = (short)(b >> 1);
                }

                return builder.ToString();
            }

            string ToBitStringInt( Int32 b )
            {
                StringBuilder builder = new();

                for (int i = 0; i < 32; i++)
                {
                    builder.Insert(0,((b % 2) == 0 ? "0" : "1"));
                    b >>= 1;
                }

                return builder.ToString();
            }
        }

        public static InputState Default => new InputState();
    }


    }


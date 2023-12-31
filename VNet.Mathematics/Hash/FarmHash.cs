﻿using System.Runtime.CompilerServices;
#if (NETCOREAPP2_1 || NETSTANDARD2_1)
using System;
#endif

namespace VNet.Mathematics.Hash
{
    public static class FarmHash
    {
        private struct uint128_t
        {
            public ulong first;
            public readonly ulong second;

            public uint128_t(ulong first, ulong second)
            {
                this.first = first;
                this.second = second;
            }
        }

        private const ulong k0 = 0xc3a5c85c97cb3127U;
        private const ulong k1 = 0xb492b66fbe98f273U;
        private const ulong k2 = 0x9ae16a3b2f90404fU;

        private const uint c1 = 0xcc9e2d51;
        private const uint c2 = 0x1b873593;

        private static uint Rotate32(uint val, int shift)
        {
            return shift == 0 ? val : (val >> shift) | (val << (32 - shift));
        }

        private static ulong Rotate64(ulong val, int shift)
        {
            return shift == 0 ? val : (val >> shift) | (val << (64 - shift));
        }

        private static uint Rotate(uint val, int shift)
        {
            return Rotate32(val, shift);
        }

        private static unsafe ulong Fetch64(byte* p)
        {
            return *(ulong*) p;
        }

        private static unsafe uint Fetch32(byte* p)
        {
            return *(uint*) p;
        }

        private static unsafe uint Fetch(byte* p)
        {
            return Fetch32(p);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static uint fmix(uint h)
        {
            h ^= h >> 16;
            h *= 0x85ebca6b;
            h ^= h >> 13;
            h *= 0xc2b2ae35;
            h ^= h >> 16;
            return h;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static unsafe uint Hash32Len0to4(byte* s, uint len, uint seed = 0)
        {
            uint b = seed;
            uint c = 9;
            for (int i = 0; i < len; i++)
            {
                b = b * c1 + s[i];
                c ^= b;
            }

            return fmix(Mur(b, Mur(len, c)));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static uint Mur(uint a, uint h)
        {
            a *= c1;
            a = Rotate32(a, 17);
            a *= c2;
            h ^= a;
            h = Rotate32(h, 19);
            return h * 5 + 0xe6546b64;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static unsafe uint Hash32Len13to24(byte* s, uint len, uint seed = 0)
        {
            uint a = Fetch(s - 4 + (len >> 1));
            uint b = Fetch(s + 4);
            uint c = Fetch(s + len - 8);
            uint d = Fetch(s + (len >> 1));
            uint e = Fetch(s);
            uint f = Fetch(s + len - 4);
            uint h = d * c1 + len + seed;
            a = Rotate(a, 12) + f;
            h = Mur(c, h) + a;
            a = Rotate(a, 3) + c;
            h = Mur(e, h) + a;
            a = Rotate(a + f, 12) + d;
            h = Mur(b ^ seed, h) + a;
            return fmix(h);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static unsafe uint Hash32Len5to12(byte* s, uint len, uint seed = 0)
        {
            uint a = len, b = len * 5, c = 9, d = b + seed;
            a += Fetch(s);
            b += Fetch(s + len - 4);
            c += Fetch(s + ((len >> 1) & 4));
            return fmix(seed ^ Mur(c, Mur(b, Mur(a, d))));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe uint Hash32(byte* s, int length)
        {
            uint len = (uint) length;
            if (len <= 24)
                return len <= 12 ? len <= 4 ? Hash32Len0to4(s, len) : Hash32Len5to12(s, len) : Hash32Len13to24(s, len);

            uint h = len, g = c1 * len, f = g;
            uint a0 = Rotate(Fetch(s + len - 4) * c1, 17) * c2;
            uint a1 = Rotate(Fetch(s + len - 8) * c1, 17) * c2;
            uint a2 = Rotate(Fetch(s + len - 16) * c1, 17) * c2;
            uint a3 = Rotate(Fetch(s + len - 12) * c1, 17) * c2;
            uint a4 = Rotate(Fetch(s + len - 20) * c1, 17) * c2;
            h ^= a0;
            h = Rotate(h, 19);
            h = h * 5 + 0xe6546b64;
            h ^= a2;
            h = Rotate(h, 19);
            h = h * 5 + 0xe6546b64;
            g ^= a1;
            g = Rotate(g, 19);
            g = g * 5 + 0xe6546b64;
            g ^= a3;
            g = Rotate(g, 19);
            g = g * 5 + 0xe6546b64;
            f += a4;
            f = Rotate(f, 19) + 113;
            uint iters = (len - 1) / 20;
            do
            {
                uint a = Fetch(s);
                uint b = Fetch(s + 4);
                uint c = Fetch(s + 8);
                uint d = Fetch(s + 12);
                uint e = Fetch(s + 16);
                h += a;
                g += b;
                f += c;
                h = Mur(d, h) + e;
                g = Mur(c, g) + a;
                f = Mur(b + e * c1, f) + d;
                f += g;
                g += f;
                s += 20;
            } while (--iters != 0);

            g = Rotate(g, 11) * c1;
            g = Rotate(g, 17) * c1;
            f = Rotate(f, 11) * c1;
            f = Rotate(f, 17) * c1;
            h = Rotate(h + g, 19);
            h = h * 5 + 0xe6546b64;
            h = Rotate(h, 17) * c1;
            h = Rotate(h + f, 19);
            h = h * 5 + 0xe6546b64;
            h = Rotate(h, 17) * c1;
            return h;
        }

        public static unsafe uint Hash32(byte[] s, int length)
        {
            fixed (byte* buf = s)
            {
                return Hash32(buf, length);
            }
        }

        public static unsafe uint Hash32(string s)
        {
            fixed (char* buffer = s)
            {
                return Hash32((byte*) buffer, s.Length * sizeof(char));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static uint128_t Uint128(ulong lo, ulong hi)
        {
            return new uint128_t(lo, hi);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static ulong ShiftMix(ulong val)
        {
            return val ^ (val >> 47);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static ulong HashLen16(ulong u, ulong v, ulong mul)
        {
            ulong a = (u ^ v) * mul;
            a ^= a >> 47;
            ulong b = (v ^ a) * mul;
            b ^= b >> 47;
            b *= mul;
            return b;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static uint128_t WeakHashLen32WithSeeds(
            ulong w, ulong x, ulong y, ulong z, ulong a, ulong b)
        {
            a += w;
            b = Rotate64(b + a + z, 21);
            ulong c = a;
            a += x;
            a += y;
            b += Rotate64(a, 44);
            return Uint128(a + z, b + c);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static unsafe uint128_t WeakHashLen32WithSeeds(
            byte* s, ulong a, ulong b)
        {
            return WeakHashLen32WithSeeds(Fetch64(s),
                Fetch64(s + 8),
                Fetch64(s + 16),
                Fetch64(s + 24),
                a,
                b);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static unsafe ulong HashLen0to16(byte* s, uint len)
        {
            if (len >= 8)
            {
                ulong mul = k2 + len * 2;
                ulong a = Fetch64(s) + k2;
                ulong b = Fetch64(s + len - 8);
                ulong c = Rotate64(b, 37) * mul + a;
                ulong d = (Rotate64(a, 25) + b) * mul;
                return HashLen16(c, d, mul);
            }

            if (len >= 4)
            {
                ulong mul = k2 + len * 2;
                ulong a = Fetch32(s);
                return HashLen16(len + (a << 3), Fetch32(s + len - 4), mul);
            }

            if (len > 0)
            {
                ushort a = s[0];
                ushort b = s[len >> 1];
                ushort c = s[len - 1];
                uint y = a + ((uint) b << 8);
                uint z = len + ((uint) c << 2);
                return ShiftMix((y * k2) ^ (z * k0)) * k2;
            }

            return k2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static unsafe ulong HashLen17to32(byte* s, uint len)
        {
            ulong mul = k2 + len * 2;
            ulong a = Fetch64(s) * k1;
            ulong b = Fetch64(s + 8);
            ulong c = Fetch64(s + len - 8) * mul;
            ulong d = Fetch64(s + len - 16) * k2;
            return HashLen16(Rotate64(a + b, 43) + Rotate64(c, 30) + d,
                a + Rotate64(b + k2, 18) + c, mul);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static ulong H(ulong x, ulong y, ulong mul, int r)
        {
            ulong a = (x ^ y) * mul;
            a ^= a >> 47;
            ulong b = (y ^ a) * mul;
            return Rotate64(b, r) * mul;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static unsafe ulong H32(byte* s, uint len, ulong mul,
            ulong seed0 = 0, ulong seed1 = 0)
        {
            ulong a = Fetch64(s) * k1;
            ulong b = Fetch64(s + 8);
            ulong c = Fetch64(s + len - 8) * mul;
            ulong d = Fetch64(s + len - 16) * k2;
            ulong u = Rotate64(a + b, 43) + Rotate64(c, 30) + d + seed0;
            ulong v = a + Rotate64(b + k2, 18) + c + seed1;
            a = ShiftMix((u ^ v) * mul);
            b = ShiftMix((v ^ a) * mul);
            return b;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static unsafe ulong HashLen33to64(byte* s, uint len)
        {
            const ulong mul0 = k2 - 30;
            ulong mul1 = k2 - 30 + 2 * len;
            ulong h0 = H32(s, 32, mul0);
            ulong h1 = H32(s + len - 32, 32, mul1);
            return (h1 * mul1 + h0) * mul1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static unsafe ulong HashLen65to96(byte* s, uint len)
        {
            const ulong mul0 = k2 - 114;
            ulong mul1 = k2 - 114 + 2 * len;
            ulong h0 = H32(s, 32, mul0);
            ulong h1 = H32(s + 32, 32, mul1);
            ulong h2 = H32(s + len - 32, 32, mul1, h0, h1);
            return (h2 * 9 + (h0 >> 17) + (h1 >> 21)) * mul1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static unsafe ulong Hash64_uo(byte* s, uint len)
        {
            const ulong seed0 = 81;
            const ulong seed1 = 0;

            ulong x = seed0;
            ulong y = seed1 * k2 + 113;
            ulong z = ShiftMix(y * k2) * k2;

            ulong v_first = seed0;
            ulong v_second = seed1;
            ulong w_first = 0;
            ulong w_second = 0;
            ulong u = x - z;
            x *= k2;
            ulong mul = k2 + (u & 0x82);

            byte* end = s + (len - 1) / 64 * 64;
            byte* last64 = end + ((len - 1) & 63) - 63;
            do
            {
                ulong a0 = Fetch64(s);
                ulong a1 = Fetch64(s + 8);
                ulong a2 = Fetch64(s + 16);
                ulong a3 = Fetch64(s + 24);
                ulong a4 = Fetch64(s + 32);
                ulong a5 = Fetch64(s + 40);
                ulong a6 = Fetch64(s + 48);
                ulong a7 = Fetch64(s + 56);
                x += a0 + a1;
                y += a2;
                z += a3;
                v_first += a4;
                v_second += a5 + a1;
                w_first += a6;
                w_second += a7;

                x = Rotate64(x, 26);
                x *= 9;
                y = Rotate64(y, 29);
                z *= mul;
                v_first = Rotate64(v_first, 33);
                v_second = Rotate64(v_second, 30);
                w_first ^= x;
                w_first *= 9;
                z = Rotate64(z, 32);
                z += w_second;
                w_second += z;
                z *= 9;

                ulong tmp = u;
                u = y;
                y = tmp;

                z += a0 + a6;
                v_first += a2;
                v_second += a3;
                w_first += a4;
                w_second += a5 + a6;
                x += a1;
                y += a7;

                y += v_first;
                v_first += x - y;
                v_second += w_first;
                w_first += v_second;
                w_second += x - y;
                x += w_second;
                w_second = Rotate64(w_second, 34);
                tmp = u;
                u = z;
                z = tmp;
                s += 64;
            } while (s != end);

            s = last64;
            u *= 9;
            v_second = Rotate64(v_second, 28);
            v_first = Rotate64(v_first, 20);
            w_first += (len - 1) & 63;
            u += y;
            y += u;
            x = Rotate64(y - x + v_first + Fetch64(s + 8), 37) * mul;
            y = Rotate64(y ^ v_second ^ Fetch64(s + 48), 42) * mul;
            x ^= w_second * 9;
            y += v_first + Fetch64(s + 40);
            z = Rotate64(z + w_first, 33) * mul;
            var v = WeakHashLen32WithSeeds(s, v_second * mul, x + w_first);
            var w = WeakHashLen32WithSeeds(s + 32, z + w_second, y + Fetch64(s + 16));
            return H(HashLen16(v.first + x, w.first ^ y, mul) + z - u,
                H(v.second + y, w.second + z, k2, 30) ^ x,
                k2,
                31);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static unsafe ulong Hash64_na(byte* s, uint len)
        {
            const ulong seed = 81;

            ulong x = seed;
            ulong y = unchecked(seed * k1 + 113);
            ulong z = ShiftMix(y * k2 + 113) * k2;
            var v = Uint128(0, 0);
            var w = Uint128(0, 0);
            x = x * k2 + Fetch64(s);

            ulong tmp;

            byte* end = s + (len - 1) / 64 * 64;
            byte* last64 = end + ((len - 1) & 63) - 63;
            do
            {
                x = Rotate64(x + y + v.first + Fetch64(s + 8), 37) * k1;
                y = Rotate64(y + v.second + Fetch64(s + 48), 42) * k1;
                x ^= w.second;
                y += v.first + Fetch64(s + 40);
                z = Rotate64(z + w.first, 33) * k1;
                v = WeakHashLen32WithSeeds(s, v.second * k1, x + w.first);
                w = WeakHashLen32WithSeeds(s + 32, z + w.second, y + Fetch64(s + 16));

                tmp = z;
                z = x;
                x = tmp;

                s += 64;
            } while (s != end);

            ulong mul = k1 + ((z & 0xff) << 1);
            s = last64;
            w.first += (len - 1) & 63;
            v.first += w.first;
            w.first += v.first;
            x = Rotate64(x + y + v.first + Fetch64(s + 8), 37) * mul;
            y = Rotate64(y + v.second + Fetch64(s + 48), 42) * mul;
            x ^= w.second * 9;
            y += v.first * 9 + Fetch64(s + 40);
            z = Rotate64(z + w.first, 33) * mul;
            v = WeakHashLen32WithSeeds(s, v.second * mul, x + w.first);
            w = WeakHashLen32WithSeeds(s + 32, z + w.second, y + Fetch64(s + 16));

            tmp = z;
            z = x;
            x = tmp;

            return HashLen16(HashLen16(v.first, w.first, mul) + ShiftMix(y) * k0 + z,
                HashLen16(v.second, w.second, mul) + x,
                mul);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe ulong Hash64(byte* s, int length)
        {
            uint len = (uint) length;
            if (len <= 32)
                return len <= 16 ? HashLen0to16(s, len) : HashLen17to32(s, len);
            if (len <= 64) return HashLen33to64(s, len);
            if (len <= 96) return HashLen65to96(s, len);
            return len <= 256 ? Hash64_na(s, len) : Hash64_uo(s, len);
        }

        public static unsafe ulong Hash64(byte[] s, int length)
        {
            fixed (byte* buf = s)
            {
                return Hash64(buf, length);
            }
        }

        public static unsafe ulong Hash64(string s)
        {
            fixed (char* buffer = s)
            {
                return Hash64((byte*) buffer, s.Length * sizeof(char));
            }
        }

#if (NETCOREAPP2_1 || NETSTANDARD2_1)
        /// <summary>
        /// Calculates the 32bit from a readonly span of byte data
        /// </summary>
        /// <param name="span">span of data to hash</param>
        /// <returns>A 32bit hash</returns>
        public static unsafe uint Hash32(ReadOnlySpan<byte> span)
        {
            fixed (byte* buf = span)
            {
                return Hash32(buf, span.Length);
            }
        }

        /// <summary>
        /// Calculates the 64bit from a readonly span of byte data
        /// </summary>
        /// <param name="span">span of data to hash</param>
        /// <returns>A 64bit hash</returns>
        public static unsafe ulong Hash64(ReadOnlySpan<byte> span)
        {
            fixed (byte* buf = span)
            {
                return Hash64(buf, span.Length);
            }
        }
#endif
    }
}
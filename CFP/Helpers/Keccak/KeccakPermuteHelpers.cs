﻿using System.Runtime.CompilerServices;

namespace c19t.SDK.CFP.Helpers.Keccak
{
    internal sealed class KeccakPermuteHelpers
    {

        private const int KeccakRounds = 24;
        private KeccakPermuteHelpers()
        {
                
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static ulong AddStateBuffer(byte[] bs, int off)
        {
            var result = bs[off]
                | (ulong)bs[off + 1] << 8
                | (ulong)bs[off + 2] << 16
                | (ulong)bs[off + 3] << 24
                | (ulong)bs[off + 4] << 32
                | (ulong)bs[off + 5] << 40
                | (ulong)bs[off + 6] << 48
                | (ulong)bs[off + 7] << 56;

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void Extract(byte[] extracted, ulong[] state)
        {
            var offset = 0;
            for (var i = 0; i < extracted.Length / 8; i++)
            {
                ExtractStateBuffer(state[i], extracted, offset);
                offset += 8;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void ExtractStateBuffer(ulong n, byte[] bs, int off)
        {
            bs[off] = (byte)(n);
            bs[off + 1] = (byte)(n >> 8);
            bs[off + 2] = (byte)(n >> 16);
            bs[off + 3] = (byte)(n >> 24);
            bs[off + 4] = (byte)(n >> 32);
            bs[off + 5] = (byte)(n >> 40);
            bs[off + 6] = (byte)(n >> 48);
            bs[off + 7] = (byte)(n >> 56);
        }

        internal static readonly ulong[] RoundConstants = new ulong[]
        {
            0x0000000000000001, 0x0000000000008082, 0x800000000000808A, 0x8000000080008000,
            0x000000000000808B, 0x0000000080000001, 0x8000000080008081, 0x8000000000008009,
            0x000000000000008A, 0x0000000000000088, 0x0000000080008009, 0x000000008000000A,
            0x000000008000808B, 0x800000000000008B, 0x8000000000008089, 0x8000000000008003,
            0x8000000000008002, 0x8000000000000080, 0x000000000000800A, 0x800000008000000A,
            0x8000000080008081, 0x8000000000008080, 0x0000000080000001, 0x8000000080008008
        };

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void Permute(ulong[] state)
        {
            var permuteState = new PermuteState();
            permuteState.Load(state);

            for (var round = 0; round < KeccakRounds; round++)
            {
                Theta(permuteState);
                RhoPi(permuteState);
                Chi(permuteState);
                Iota(permuteState, round);
            }

            permuteState.SetState(state);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void Theta(PermuteState permuteState)
        {
            permuteState.C0 = permuteState.A00 ^ permuteState.A05 ^ permuteState.A10 ^ permuteState.A15 ^ permuteState.A20;
            permuteState.C1 = permuteState.A01 ^ permuteState.A06 ^ permuteState.A11 ^ permuteState.A16 ^ permuteState.A21;
            var c2 = permuteState.A02 ^ permuteState.A07 ^ permuteState.A12 ^ permuteState.A17 ^ permuteState.A22;
            var c3 = permuteState.A03 ^ permuteState.A08 ^ permuteState.A13 ^ permuteState.A18 ^ permuteState.A23;
            var c4 = permuteState.A04 ^ permuteState.A09 ^ permuteState.A14 ^ permuteState.A19 ^ permuteState.A24;

            var d0 = ShiftULongLeft(permuteState.C1, 1) ^ c4;
            var d1 = ShiftULongLeft(c2, 1) ^ permuteState.C0;
            var d2 = ShiftULongLeft(c3, 1) ^ permuteState.C1;
            var d3 = ShiftULongLeft(c4, 1) ^ c2;
            var d4 = ShiftULongLeft(permuteState.C0, 1) ^ c3;

            permuteState.A00 ^= d0;
            permuteState.A05 ^= d0;
            permuteState.A10 ^= d0;
            permuteState.A15 ^= d0;
            permuteState.A20 ^= d0;
            permuteState.A01 ^= d1;
            permuteState.A06 ^= d1;
            permuteState.A11 ^= d1;
            permuteState.A16 ^= d1;
            permuteState.A21 ^= d1;
            permuteState.A02 ^= d2;
            permuteState.A07 ^= d2;
            permuteState.A12 ^= d2;
            permuteState.A17 ^= d2;
            permuteState.A22 ^= d2;
            permuteState.A03 ^= d3;
            permuteState.A08 ^= d3;
            permuteState.A13 ^= d3;
            permuteState.A18 ^= d3;
            permuteState.A23 ^= d3;
            permuteState.A04 ^= d4;
            permuteState.A09 ^= d4;
            permuteState.A14 ^= d4;
            permuteState.A19 ^= d4;
            permuteState.A24 ^= d4;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void RhoPi(PermuteState permuteState)
        {
            permuteState.C1 = ShiftULongLeft(permuteState.A01, 1);

            permuteState.A01 = ShiftULongLeft(permuteState.A06, 44);
            permuteState.A06 = ShiftULongLeft(permuteState.A09, 20);
            permuteState.A09 = ShiftULongLeft(permuteState.A22, 61);
            permuteState.A22 = ShiftULongLeft(permuteState.A14, 39);
            permuteState.A14 = ShiftULongLeft(permuteState.A20, 18);
            permuteState.A20 = ShiftULongLeft(permuteState.A02, 62);
            permuteState.A02 = ShiftULongLeft(permuteState.A12, 43);
            permuteState.A12 = ShiftULongLeft(permuteState.A13, 25);
            permuteState.A13 = ShiftULongLeft(permuteState.A19, 08);
            permuteState.A19 = ShiftULongLeft(permuteState.A23, 56);
            permuteState.A23 = ShiftULongLeft(permuteState.A15, 41);
            permuteState.A15 = ShiftULongLeft(permuteState.A04, 27);
            permuteState.A04 = ShiftULongLeft(permuteState.A24, 14);
            permuteState.A24 = ShiftULongLeft(permuteState.A21, 02);
            permuteState.A21 = ShiftULongLeft(permuteState.A08, 55);
            permuteState.A08 = ShiftULongLeft(permuteState.A16, 45);
            permuteState.A16 = ShiftULongLeft(permuteState.A05, 36);
            permuteState.A05 = ShiftULongLeft(permuteState.A03, 28);
            permuteState.A03 = ShiftULongLeft(permuteState.A18, 21);
            permuteState.A18 = ShiftULongLeft(permuteState.A17, 15);
            permuteState.A17 = ShiftULongLeft(permuteState.A11, 10);
            permuteState.A11 = ShiftULongLeft(permuteState.A07, 06);
            permuteState.A07 = ShiftULongLeft(permuteState.A10, 03);

            permuteState.A10 = permuteState.C1;

        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void Chi(PermuteState permuteState)
        {
            permuteState.C0 = permuteState.A00 ^ (~permuteState.A01 & permuteState.A02);
            permuteState.C1 = permuteState.A01 ^ (~permuteState.A02 & permuteState.A03);
            permuteState.A02 ^= ~permuteState.A03 & permuteState.A04;
            permuteState.A03 ^= ~permuteState.A04 & permuteState.A00;
            permuteState.A04 ^= ~permuteState.A00 & permuteState.A01;
            permuteState.A00 = permuteState.C0;
            permuteState.A01 = permuteState.C1;

            permuteState.C0 = permuteState.A05 ^ (~permuteState.A06 & permuteState.A07);
            permuteState.C1 = permuteState.A06 ^ (~permuteState.A07 & permuteState.A08);
            permuteState.A07 ^= ~permuteState.A08 & permuteState.A09;
            permuteState.A08 ^= ~permuteState.A09 & permuteState.A05;
            permuteState.A09 ^= ~permuteState.A05 & permuteState.A06;
            permuteState.A05 = permuteState.C0;
            permuteState.A06 = permuteState.C1;

            permuteState.C0 = permuteState.A10 ^ (~permuteState.A11 & permuteState.A12);
            permuteState.C1 = permuteState.A11 ^ (~permuteState.A12 & permuteState.A13);
            permuteState.A12 ^= ~permuteState.A13 & permuteState.A14;
            permuteState.A13 ^= ~permuteState.A14 & permuteState.A10;
            permuteState.A14 ^= ~permuteState.A10 & permuteState.A11;
            permuteState.A10 = permuteState.C0;
            permuteState.A11 = permuteState.C1;

            permuteState.C0 = permuteState.A15 ^ (~permuteState.A16 & permuteState.A17);
            permuteState.C1 = permuteState.A16 ^ (~permuteState.A17 & permuteState.A18);
            permuteState.A17 ^= ~permuteState.A18 & permuteState.A19;
            permuteState.A18 ^= ~permuteState.A19 & permuteState.A15;
            permuteState.A19 ^= ~permuteState.A15 & permuteState.A16;
            permuteState.A15 = permuteState.C0;
            permuteState.A16 = permuteState.C1;

            permuteState.C0 = permuteState.A20 ^ (~permuteState.A21 & permuteState.A22);
            permuteState.C1 = permuteState.A21 ^ (~permuteState.A22 & permuteState.A23);
            permuteState.A22 ^= ~permuteState.A23 & permuteState.A24;
            permuteState.A23 ^= ~permuteState.A24 & permuteState.A20;
            permuteState.A24 ^= ~permuteState.A20 & permuteState.A21;
            permuteState.A20 = permuteState.C0;
            permuteState.A21 = permuteState.C1;

        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void Iota(PermuteState permuteState, int round)
        {
            permuteState.A00 ^= RoundConstants[round];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static ulong ShiftULongLeft(ulong x, byte y) => (x << y) | (x >> (64 - y));
    }
}

namespace c19t.SDK.CFP.Helpers.Keccak
{
    /// <summary>
    /// This class (and associated classes) are copies from https://github.com/Devillierss/SHA3Core.
    /// REMOVE ME: Wait for release of nuget package targeting .net standard 2.0 and use it.
    /// </summary>
    class Keccak256 : Keccak1600
    {
        public Keccak256() : base(256)
        {
        }

        public string Hash(string stringToHash)
        {
            var encodedBytes = Converters.ConvertStringToBytes(stringToHash);

            base.Initialize(0x01);
            base.Absorb(encodedBytes, 0, encodedBytes.Length);
            base.Partial(encodedBytes, 0, encodedBytes.Length);

            var byteResult = base.Squeeze();

            return Converters.ConvertBytesToStringHash(byteResult);
        }

        public string Hash(byte[] bytesToHash)
        {
            base.Initialize(0x01);
            base.Absorb(bytesToHash, 0, bytesToHash.Length);
            base.Partial(bytesToHash, 0, bytesToHash.Length);

            var byteResult = base.Squeeze();

            return Converters.ConvertBytesToStringHash(byteResult);
        }
    }
}

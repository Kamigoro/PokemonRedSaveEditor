using PokemonSaveEditor.Libraries.RamOffSet;

namespace PokemonSaveEditor.Libraries.Utils
{
    public static class RamChecksum
    {
        /// <summary>
        /// Set the checksum value at the correct place in the ram
        /// </summary>
        /// <param name="checksum"></param>
        /// <param name="ram"></param>
        /// <returns>The ram with the new checksum</returns>
        public static byte[] SetRamCheckSum(byte checksum, byte[] ram)
        {
            ram[ChecksumRamOffset.Start] = checksum;
            return ram;
        }

        /// <summary>
        /// Returns the checksum value of the ram
        /// </summary>
        /// <param name="ram"></param>
        /// <returns>Single byte containing the checksum value</returns>
        public static byte GetRamChecksum(byte[] ram)
        {
            return ram[ChecksumRamOffset.Start];
        }

        /// <summary>
        /// Calculate the checksum for the data part
        /// </summary>
        /// <param name="ram"></param>
        /// <returns>A single byte containing the checksum for every byte from 0x2598 to 0x3521</returns>
        public static byte CalculateChecksum(byte[] ram)
        {
            byte checksum = 0xff;
            for (var i = PlayerNameRamOffset.Start; i < ChecksumRamOffset.Start; i++)
            {
                checksum -= ram[i];
            }
            return checksum;
        }
    }
}

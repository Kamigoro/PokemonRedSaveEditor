using PokemonSaveEditor.Libraries.RamOffSet;

namespace PokemonSaveEditor.Libraries.Utils
{
    public static class RamChecksum
    {
        /// <summary>
        /// Set the checksum value at the correct place in the save
        /// </summary>
        /// <param name="checksum"></param>
        /// <param name="save"></param>
        /// <returns>The save with the new checksum</returns>
        public static byte[] SetRamCheckSum(byte checksum, byte[] save)
        {
            save[ChecksumRamOffset.Start] = checksum;
            return save;
        }

        /// <summary>
        /// Returns the checksum value of the save
        /// </summary>
        /// <param name="save"></param>
        /// <returns>Single byte containing the checksum value</returns>
        public static byte GetRamChecksum(byte[] save)
        {
            return save[ChecksumRamOffset.Start];
        }

        /// <summary>
        /// Calculate the checksum for the data part
        /// </summary>
        /// <param name="save"></param>
        /// <returns>A single byte containing the checksum for every byte from 0x2598 to 0x3521</returns>
        public static byte CalculateChecksum(byte[] save)
        {
            byte checksum = 0xff;
            for (var i = PlayerNameRamOffset.Start; i < ChecksumRamOffset.Start; i++)
            {
                checksum -= save[i];
            }
            return checksum;
        }
    }
}

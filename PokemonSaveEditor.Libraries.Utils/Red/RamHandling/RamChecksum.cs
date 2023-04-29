using PokemonSaveEditor.Libraries.Red.RamOffSet;

namespace PokemonSaveEditor.Libraries.Utils.Red.RamHandling
{
    /// <summary>
    /// Provides methods for calculating and setting checksum values in byte arrays representing game saves.
    /// </summary>
    public static class RamChecksum
    {
        /// <summary>
        /// Sets the checksum value at the correct place in the save.
        /// </summary>
        /// <param name="save">The byte array representing the game save.</param>
        /// <param name="checksum">The checksum value to set.</param>
        /// <returns>The updated byte array with the new checksum value.</returns>
        public static byte[] SetRamCheckSum(ref byte[] save, byte checksum)
        {
            save[ChecksumRamOffset.Start] = checksum;
            return save;
        }

        /// <summary>
        /// Returns the checksum value of the save.
        /// </summary>
        /// <param name="save">The byte array representing the game save.</param>
        /// <returns>The checksum value as a single byte.</returns>
        public static byte GetRamChecksum(byte[] save)
        {
            return save[ChecksumRamOffset.Start];
        }

        /// <summary>
        /// Calculates the checksum for the data part of the save.
        /// </summary>
        /// <param name="save">The byte array representing the game save.</param>
        /// <returns>The calculated checksum as a single byte.</returns>
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

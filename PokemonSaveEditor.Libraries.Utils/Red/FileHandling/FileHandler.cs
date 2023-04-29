using PokemonSaveEditor.Libraries.Utils.Red.RamHandling;

namespace PokemonSaveEditor.Libraries.Utils.Red.FileHandling
{
    /// <summary>
    /// Validates that the byte array represents a valid save file for Pokemon Red.
    /// </summary>
    /// <param name="file">The byte array of the save file to validate.</param>
    /// <returns>A tuple containing a bool indicating whether the file is valid and a string with an error message if the file is invalid.</returns>
    public static class FileHandler
    {
        /// <summary>
        /// Validates that the byte array represents a valid save file for Pokemon Red.
        /// </summary>
        /// <param name="file">The byte array of the save file to validate.</param>
        /// <returns>A tuple containing a bool indicating whether the file is valid and a string with an error message if the file is invalid.</returns>
        public static (bool, string) ValidateSaveFile(byte[] file)
        {
            if (file.Length != 32768)
            {
                return (false, "Save file size is incorrect.");
            }
            if (RamChecksum.GetRamChecksum(file) != RamChecksum.CalculateChecksum(file))
            {
                return (false, "Save file is invalid");
            }
            return (true, string.Empty);
        }
    }
}

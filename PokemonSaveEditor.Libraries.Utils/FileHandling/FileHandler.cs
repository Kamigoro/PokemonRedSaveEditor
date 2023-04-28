using Microsoft.AspNetCore.Http;

namespace PokemonSaveEditor.Libraries.Utils
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
            if(RamChecksum.GetRamChecksum(file) != RamChecksum.CalculateChecksum(file))
            {
                return (false, "Save file is invalid");
            }
            return (true, string.Empty);
        }

        /// <summary>
        /// Validates that the IFormFile represents a valid save file for Pokemon Red.
        /// </summary>
        /// <param name="file">The IFormFile of the save file to validate.</param>
        /// <returns>A tuple containing a bool indicating whether the file is valid and a string with an error message if the file is invalid.</returns>
        public static (bool, string) ValidateSaveFile(IFormFile file)
        {
            if (!file.FileName.EndsWith(".sav"))
            {
                return (false, "Save file has not the good extension.");
            }
            if (file.Length != 32768)
            {
                return (false, "Save file size is incorrect.");
            }
            return (true, String.Empty);
        }

        /// <summary>
        /// Validates that the file at the specified path represents a valid save file for Pokemon Red.
        /// </summary>
        /// <param name="saveFilePath">The path to the save file to validate.</param>
        /// <returns>A tuple containing a bool indicating whether the file is valid and a string with an error message if the file is invalid.</returns>
        public static (bool, string) ValidateSaveFile(string saveFilePath)
        {
            if (!saveFilePath.EndsWith(".sav"))
            {
                return (false, "Save file has not the good extension.");
            }
            if (!File.Exists(saveFilePath))
            {
                return (false, "Path does not exist.");
            }
            if(File.ReadAllBytes(saveFilePath).Length != 32768)
            {
                return (false, "Save file size is incorrect.");
            }

            return (true, string.Empty);
        }
    }
}

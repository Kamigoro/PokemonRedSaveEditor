using Microsoft.AspNetCore.Http;

namespace PokemonSaveEditor.Libraries.Utils
{
    public static class FileHandler
    {
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

namespace PokemonSaveEditor.Libraries.Utils
{
    public static class FileHandler
    {
        public static (bool, string) ValidateSaveFile(string saveFilePath)
        {
            string errorMessage = string.Empty;

            if (!saveFilePath.EndsWith(".sav"))
            {
                errorMessage = "Save file has not the good extension.";
                return (false, errorMessage);
            }
            if (!File.Exists(saveFilePath))
            {
                errorMessage = "Path does not exist.";
                return (false, errorMessage);
            }
            if(File.ReadAllBytes(saveFilePath).Length != 32768)
            {
                errorMessage = "Save file size is incorrect.";
                return (false, errorMessage);
            }

            return (true, errorMessage);
        }
    }
}

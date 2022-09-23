namespace PokemonSaveEditor.Libraries.Utils
{
    public static class FileHandler
    {
        public static (bool, string) ValidateSaveFile(string saveFilePath)
        {
            string errorMessage = string.Empty;

            if (!saveFilePath.EndsWith(".sav"))
            {
                errorMessage = "Le chemin spécifié est incorrect.";
                return (false, errorMessage);
            }
            if (!File.Exists(saveFilePath))
            {
                errorMessage = "Le chemin spécifié est incorrect.";
                return (false, errorMessage);
            }

            return (true, errorMessage);
        }
    }
}

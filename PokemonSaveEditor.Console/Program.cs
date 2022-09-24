
namespace PokemonSaveEditor.Console
{
    using PokemonSaveEditor.Libraries.Utils;
    using PokemonSaveEditor.Libraries.Utils.DataModification;
    using System;
    class Program
    {
        static int Main(string[] args)
        {
            Console.WriteLine("Please specify the path of the save to modify : ");
            var saveFilePath = Console.ReadLine();

            var (success, errorMessage) = FileHandler.ValidateSaveFile(saveFilePath);
            if (!success)
            {
                Console.WriteLine(errorMessage);
                return -1;
            }

            byte[] ram = File.ReadAllBytes(saveFilePath);

            ram = PlayerNameManager.SetPlayerName("Benoit", ram);

            var newChecksum = RamChecksum.CalculateChecksum(ram);
            ram = RamChecksum.SetRamCheckSum(newChecksum, ram);

            File.WriteAllBytes(saveFilePath.Replace("-backup.sav", ".sav"), ram);

            return 0;
        }
    }
}

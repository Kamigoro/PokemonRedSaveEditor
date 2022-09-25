
namespace PokemonSaveEditor.Console
{
    using PokemonSaveEditor.Libraries.Utils;
    using System;
    public class Program
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

            byte[] save = File.ReadAllBytes(saveFilePath);

            var newChecksum = RamChecksum.CalculateChecksum(save);
            save = RamChecksum.SetRamCheckSum(newChecksum, save);

            File.WriteAllBytes(saveFilePath, save);

            return 0;
        }
    }
}

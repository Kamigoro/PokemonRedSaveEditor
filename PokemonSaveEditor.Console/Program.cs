
namespace PokemonSaveEditor.Console
{
    using PokemonSaveEditor.Libraries.Utils;
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

            Console.WriteLine("How much money do you want ?");
            var moneyString = Console.ReadLine();
            if(!Int32.TryParse(moneyString, out int money) || money > 999999)
            {
                Console.WriteLine("Text entered is either invalid or superior to 999999.");
                return -1;
            }
            
            byte[] ram = File.ReadAllBytes(saveFilePath);

            ram = MoneyManager.SetMoney(money, ram);
            var newChecksum = RamChecksum.CalculateChecksum(ram);
            ram = RamChecksum.SetRamCheckSum(newChecksum, ram);

            File.WriteAllBytes(saveFilePath, ram);

            return 0;
        }
    }
}

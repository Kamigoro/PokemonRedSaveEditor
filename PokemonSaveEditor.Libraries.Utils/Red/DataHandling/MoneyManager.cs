using PokemonSaveEditor.Libraries.Red.RamOffSet;

namespace PokemonSaveEditor.Libraries.Utils.Red.DataHandling
{
    public static class MoneyManager
    {
        /// <summary>
        /// Set the desired amount of money for the player.
        /// </summary>
        /// <param name="save">The save file to modify.</param>
        /// <param name="money">The amount of money to set, between 0 and 999,999.</param>
        /// <returns>The modified save file.</returns>
        /// <exception cref="ArgumentException">Thrown if the specified amount of money is not between 0 and 999,999.</exception>
        public static byte[] SetMoney(ref byte[] save, int money)
        {
            if (money > 999999 || money < 0)
            {
                throw new ArgumentException("Money should be between 0 and 999 999 credits.");
            }

            var moneyByteArray = GetByteForEachDigit(money);

            var moneyBytes = ConvertMoneyToThreeBytes(moneyByteArray);
            int counter = 0;
            for (int i = MoneyRamOffset.Start; i < MoneyRamOffset.End; i++)
            {
                save[i] = moneyBytes[counter];
                counter++;
            }
            return save;
        }

        /// <summary>
        /// Get the current amount of money the player has.
        /// </summary>
        /// <param name="save">The save file to check.</param>
        /// <returns>The amount of money the player has.</returns>
        public static int GetMoney(byte[] save)
        {
            var moneyBytes = new byte[3];
            for (int i = MoneyRamOffset.Start; i < MoneyRamOffset.End; i++)
            {
                moneyBytes[i - MoneyRamOffset.Start] = save[i];
            }

            var digits = ConvertThreeBytesToSixDigits(moneyBytes);

            var numberString = string.Empty;
            foreach (var digit in digits)
            {
                numberString = numberString + digit.ToString();
            }

            return int.Parse(numberString);
        }

        /// <summary>
        /// Converts an integer into a byte array of six, where each byte contains a digit.
        /// </summary>
        /// <param name="money">The integer to convert.</param>
        /// <returns>A byte array of six bytes, each representing a digit.</returns>
        private static byte[] GetByteForEachDigit(int money)
        {
            var moneyString = money.ToString();

            //If there's nopt 6 digit, we pad with 0
            // '127' => '000127'
            if (moneyString.Length < 6)
            {
                moneyString = moneyString.PadLeft(6, '0');
            }

            // Store the first byte of each integer
            var digitsAsSingleByte = new byte[6];
            for (int i = 0; i < 6; i++)
            {
                digitsAsSingleByte[i] = BitConverter.GetBytes(uint.Parse(moneyString[i].ToString()))[0];
            }

            return digitsAsSingleByte;
        }

        /// <summary>
        /// Converts a byte array of six digits into three bytes, where each digit takes up four bits of a byte.
        /// </summary>
        /// <param name="digitsAsSingleByte">The byte array of six digits.</param>
        /// <returns>A byte array of three bytes.</returns>
        private static byte[] ConvertMoneyToThreeBytes(byte[] digitsAsSingleByte)
        {
            //Trick to transform 2 * 8 bits integer
            //In a single byte integer
            var finalMoneyBytes = new List<byte>();
            for (int i = 1; i < 6; i = i + 2)
            {
                var firstNumberInByte = digitsAsSingleByte[i - 1];
                var secondNumberInByte = digitsAsSingleByte[i];

                var shiftedFirstNumber = BitConverter.GetBytes(firstNumberInByte << 4)[0];

                var combinedByte = BitConverter.GetBytes(shiftedFirstNumber | secondNumberInByte)[0];

                finalMoneyBytes.Add(combinedByte);
            }

            return finalMoneyBytes.ToArray();
        }

        /// <summary>
        /// Takes the three bytes representing the player's money and converts them to an array of six digits.
        /// </summary>
        /// <param name="moneyStoredInRam">The three bytes representing the player's money.</param>
        /// <returns>An array containing six integers representing digits.</returns>
        private static int[] ConvertThreeBytesToSixDigits(byte[] moneyStoredInRam)
        {
            var digits = new int[6];
            for (int i = 0; i < 6; i = i + 2)
            {
                var digitsCombinedByte = moneyStoredInRam[i / 2];

                var firstDigit = digitsCombinedByte >> 4;
                var secondDigitByte = BitConverter.GetBytes(digitsCombinedByte << 4)[0];
                var secondDigit = secondDigitByte >> 4;

                digits[i] = firstDigit;
                digits[i + 1] = secondDigit;
            }

            return digits;
        }
    }
}

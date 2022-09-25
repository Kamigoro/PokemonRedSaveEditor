using PokemonSaveEditor.Libraries.RamOffSet;

namespace PokemonSaveEditor.Libraries.Utils
{
    public static class MoneyManager
    {
        /// <summary>
        /// Set the desired money
        /// </summary>
        /// <param name="money"></param>
        /// <param name="save"></param>
        /// <returns>The save with the new money</returns>
        /// <exception cref="ArgumentException">Money wanted is not between 0 and 999 999</exception>
        public static byte[] SetMoney(int money, byte[] save)
        {
            if(money > 999999 || money < 0 )
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
        /// Returns the money actually stored in save
        /// </summary>
        /// <param name="save"></param>
        /// <returns>The player money</returns>
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

            return Int32.Parse(numberString);
        }

        /// <summary>
        /// Transform a integer into a byte array of 6, each byte containing a digit
        /// </summary>
        /// <param name="money"></param>
        /// <returns></returns>
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
                digitsAsSingleByte[i] = BitConverter.GetBytes(UInt32.Parse(moneyString[i].ToString()))[0];
            }

            return digitsAsSingleByte;
        }

        /// <summary>
        /// Convert a 6 digit byte array into 3 bytes, each digit is 4 bits
        /// </summary>
        /// <param name="digitsAsSingleByte"></param>
        /// <returns></returns>
        private static byte[] ConvertMoneyToThreeBytes(byte[] digitsAsSingleByte)
        {
            //Trick to transform 2 * 8 bits integer
            //In a single byte integer
            var finalMoneyBytes = new List<byte>();
            for (int i = 1; i < 6 ; i = i+2)
            {
                var firstNumberInByte = digitsAsSingleByte[i-1];
                var secondNumberInByte = digitsAsSingleByte[i];

                var shiftedFirstNumber = BitConverter.GetBytes(firstNumberInByte << 4)[0];

                var combinedByte = BitConverter.GetBytes(shiftedFirstNumber | secondNumberInByte)[0];

                finalMoneyBytes.Add(combinedByte);
            }

            return finalMoneyBytes.ToArray();
        }

        /// <summary>
        /// Takes the 3 bytes representing money and convert them to an array of 6 digits.
        /// </summary>
        /// <param name="moneyStoredInRam"></param>
        /// <returns>An array containing 6 integers representing digits</returns>
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

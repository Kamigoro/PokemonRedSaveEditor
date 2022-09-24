using PokemonSaveEditor.Libraries.RamOffSet;

namespace PokemonSaveEditor.Libraries.Utils
{
    public static class MoneyManager
    {
        /// <summary>
        /// Set the desired money
        /// </summary>
        /// <param name="money"></param>
        /// <param name="ram"></param>
        /// <returns>The ram with the new money</returns>
        public static byte[] SetMoney(int money, byte[] ram)
        {
            if(money > 999999 || money < 0 )
            {
                throw new ArgumentException("Money should be between 0 and 999 999 credits.");
            }

            var moneyByteArray = GetByteForEachDigit(money);

            var moneyBytes = ReduceMoneyToThreeBytes(moneyByteArray);
            int counter = 0;
            for (int i = MoneyRamOffset.Start; i < MoneyRamOffset.End; i++)
            {
                ram[i] = moneyBytes[counter];
                counter++;
            }
            return ram;
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
        private static byte[] ReduceMoneyToThreeBytes(byte[] digitsAsSingleByte)
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
    }
}

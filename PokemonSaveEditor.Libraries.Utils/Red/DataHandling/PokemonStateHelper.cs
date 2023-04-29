namespace PokemonSaveEditor.Libraries.Utils.Red.DataHandling
{
    /// <summary>
    /// Class that provides static methods to get or change the state (seen/owned) of a specific Pokemon in the saved game data.
    /// </summary>
    public static class PokemonStateHelper
    {
        /// <summary>
        /// Gets the byte and bit position of a Pokemon based on its pokedex entry number.
        /// </summary>
        /// <param name="pokemonNumber"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static (int byteNumber, int bitPosition) GetPokemonByteAndBitPosition(int pokemonNumber)
        {
            if (pokemonNumber < 1 || pokemonNumber > 151)
                throw new ArgumentOutOfRangeException("Pokemon number should be between 1 and 151.");
            //0 based entry
            pokemonNumber--;
            var pokemonByteNumber = pokemonNumber / 8;
            var pokemonBitPosition = pokemonNumber % 8;
            return (pokemonByteNumber, pokemonBitPosition);
        }

        /// <summary>
        /// Changes the state of a specific Pokemon in the saved game data.
        /// </summary>
        /// <param name="save"></param>
        /// <param name="pokemonNumber"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public static byte[] ChangePokemonState(ref byte[] save, int ramOffset, int pokemonNumber, bool state)
        {
            var (pokemonByteNumber, pokemonBitPosition) = GetPokemonByteAndBitPosition(pokemonNumber);
            var byteInWhichPokemonIs = save[ramOffset + pokemonByteNumber];
            if (state)
            {
                byteInWhichPokemonIs |= (byte)(1 << pokemonBitPosition);
            }
            else
            {
                byteInWhichPokemonIs &= (byte)~(1 << pokemonBitPosition);
            }
            save[ramOffset + pokemonByteNumber] = byteInWhichPokemonIs;
            return save;
        }

        public static bool GetPokemonState(byte[] save, int ramOffset, int pokemonNumber)
        {
            var (pokemonByteNumber, pokemonBitPosition) = GetPokemonByteAndBitPosition(pokemonNumber);
            var byteInWhichPokemonIs = save[ramOffset + pokemonByteNumber];
            bool state;
            var intermediateAnswer = byteInWhichPokemonIs >> pokemonBitPosition;
            if (intermediateAnswer == 1)
            {
                state = true;
            }
            else
            {
                state = intermediateAnswer % 2 != 0;
            }
            return state;
        }
    }
}

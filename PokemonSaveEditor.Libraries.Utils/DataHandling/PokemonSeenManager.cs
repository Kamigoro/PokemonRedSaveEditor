using PokemonSaveEditor.Libraries.RamOffSet;

namespace PokemonSaveEditor.Libraries.Utils.DataHandling
{
    /// <summary>
    /// Provides a static method to check if a specific Pokemon number has been seen or to make it seen in a given saved game data.
    /// </summary>
    public static class PokemonSeenManager
    {
        /// <summary>
        /// Sets a specific Pokemon as seen/not seen in the saved game data.
        /// </summary>
        /// <param name="save">The saved game data where the pokemon sight state will be set.</param>
        /// <param name="seen">The sight state to set for the Pokemon.</param>
        /// <param name="pokemonNumber">The number of the Pokemon to mark as seen or not.</param>
        /// <returns>The modified save file.</returns>
        public static byte[] SetSightState(ref byte[] save, int pokemonNumber, bool seen = true)
        {
            if (pokemonNumber < 1 || pokemonNumber > 151)
                throw new ArgumentOutOfRangeException("Pokemon number should be between 1 and 151.");

            //0 based entry
            pokemonNumber--;

            var pokemonByteNumber = pokemonNumber / 8;
            var pokemonBitPosition = pokemonNumber % 8;

            var byteInWhichPokemonIs = save[PokemonSeenRamOffset.Start + pokemonByteNumber];

            if (seen)
            {
                byteInWhichPokemonIs |= (byte)(1 << pokemonBitPosition);
            }
            else
            {
                byteInWhichPokemonIs &= (byte)(~(1 << pokemonBitPosition));
            }

            save[PokemonSeenRamOffset.Start + pokemonByteNumber] = byteInWhichPokemonIs;
            return save;
        }

        /// <summary>
        /// Determines whether a specific Pokemon is seen in the saved game data.
        /// </summary>
        /// <param name="pokemonNumber">The number of the Pokemon to check sight for (1 to 151).</param>
        /// <param name="save">The saved game data where sight will be checked.</param>
        /// <returns>True if the Pokemon has been seen, false otherwise.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the Pokemon number is not within the valid range of 1 to 151.</exception>
        public static bool IsSeen(byte[] save, int pokemonNumber)
        {

            if (pokemonNumber < 1 || pokemonNumber > 151)
                throw new ArgumentOutOfRangeException("Pokemon number should be between 1 and 151.");

            //0 based entry
            pokemonNumber--;

            var pokemonByteNumber = pokemonNumber / 8;
            var pokemonBitPosition = pokemonNumber % 8;

            var byteInWhichPokemonIs = save[PokemonSeenRamOffset.Start + pokemonByteNumber];

            bool isSeen;
            var intermediateAnswer = byteInWhichPokemonIs >> pokemonBitPosition;

            if (intermediateAnswer == 1)
            {
                isSeen = true;
            }
            else
            {
                isSeen = intermediateAnswer % 2 != 0;
            }

            return isSeen;
        }
    }
}

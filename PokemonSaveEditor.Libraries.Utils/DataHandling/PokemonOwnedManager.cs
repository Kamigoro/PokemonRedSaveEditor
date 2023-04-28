using PokemonSaveEditor.Libraries.RamOffSet;

namespace PokemonSaveEditor.Libraries.Utils.DataHandling
{
    /// <summary>
    /// Provides a static method to check if a specific Pokemon number is owned in a given saved game data.
    /// </summary>
    public class PokemonOwnedManager
    {
        /// <summary>
        /// Determines whether a specific Pokemon is owned in the saved game data.
        /// </summary>
        /// <param name="pokemonNumber">The number of the Pokemon to check ownership for (1 to 151).</param>
        /// <param name="save">The saved game data where ownership will be checked.</param>
        /// <returns>True if the Pokemon is owned, false otherwise.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the Pokemon number is not within the valid range of 1 to 151.</exception>
        public static bool IsOwned(int pokemonNumber, byte[] save)
        {
            
            if(pokemonNumber < 1 || pokemonNumber > 151)
            {
                throw new ArgumentOutOfRangeException("Pokemon number should be between 1 and 151.");
            }

            //0 based entry
            pokemonNumber--;

            var pokemonByteNumber = pokemonNumber / 8;
            var pokemonBitPosition = pokemonNumber % 8;

            var byteInWhichPokemonIs = save[PokemonOwnedRamOffset.Start + pokemonByteNumber];

            bool isOwned = false;
            var intermediateAnswer = byteInWhichPokemonIs >> pokemonBitPosition;

            if(intermediateAnswer == 1)
            {
                isOwned = true;
            }
            else
            {
                isOwned = intermediateAnswer % 2 != 0;
            }

            return isOwned;
        }
    }
}

using PokemonSaveEditor.Libraries.Red.RamOffSet;

namespace PokemonSaveEditor.Libraries.Utils.Red.DataHandling
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
            return PokemonStateHelper.ChangePokemonState(ref save, PokemonSeenRamOffset.Start, pokemonNumber, seen);
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
            return PokemonStateHelper.GetPokemonState(save, PokemonSeenRamOffset.Start, pokemonNumber);
        }
    }
}

using PokemonSaveEditor.Libraries.RamOffSet;

namespace PokemonSaveEditor.Libraries.Utils.DataHandling
{
    public class PokemonOwnedManager
    {
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

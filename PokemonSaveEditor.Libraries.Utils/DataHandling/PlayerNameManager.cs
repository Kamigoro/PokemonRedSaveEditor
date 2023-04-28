using PokemonSaveEditor.Libraries.RamOffSet;
using PokemonSaveEditor.Libraries.Text;

namespace PokemonSaveEditor.Libraries.Utils.DataHandling
{
    /// <summary>
    /// Provides functionality for setting and retrieving the player's name in a game save file.
    /// </summary>
    public static class PlayerNameManager
    {
        /// <summary>
        /// Change the player's name in a save file.
        /// </summary>
        /// <param name="save">The byte array representing the save file to modify.</param>
        /// <param name="name">The new name to set for the player.</param>
        /// <returns>The modified save file byte array with the new player's name inside.</returns>
        /// <exception cref="ArgumentException">Thrown when the name is null or empty, or when the name is longer than 7 characters.</exception>
        public static byte[] SetPlayerName(ref byte[] save, string name)
        {
            if(string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Name can't be empty.");
            }
            else if(name.Length > 7)
            {
                throw new ArgumentException("Name is longer than 7 characters.");
            }

            var nameByteArray = GetByteArray(name);

            for (int i = PlayerNameRamOffset.Start; i < PlayerNameRamOffset.End; i++)
            {
                save[i] = nameByteArray[i-PlayerNameRamOffset.Start];
            }

            return save;
        }

        /// <summary>
        /// Returns the player's name stored in a save file.
        /// </summary>
        /// <param name="save">The byte array representing the save file to read.</param>
        /// <returns>The player's name as a string.</returns>
        public static string GetPlayerName(byte[] save)
        {
            byte[] nameByteArray = new byte[11];
            string name = string.Empty;
            for (int i = PlayerNameRamOffset.Start; i < PlayerNameRamOffset.End; i++)
            {
                nameByteArray[i - PlayerNameRamOffset.Start] = save[i];
            }
            foreach (var characterByte in nameByteArray)
            {
                var correspondingEntry = FrenchGermanCharacterEncoding.Characters.FirstOrDefault(k => k.Value == characterByte);
                if (!string.IsNullOrEmpty(correspondingEntry.Key))
                {
                    name += correspondingEntry.Key;
                }
            }
            return name;
        }

        private static byte[] GetByteArray(string name)
        {
            var byteArray = new byte[11];

            for (int i = 0; i < name.Length; i++)
            {
                if (!FrenchGermanCharacterEncoding.Characters.TryGetValue(name[i].ToString(), out byte characterEncoding))
                {
                    throw new ArgumentException($"This character {name[i]} is not supported by encoder.");
                }
                
                byteArray[i] = characterEncoding;
            }

            byteArray[name.Length] = FrenchGermanCharacterEncoding.EndOfString;

            return byteArray;
        }
    }
}

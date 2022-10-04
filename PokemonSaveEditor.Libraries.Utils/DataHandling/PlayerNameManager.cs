using PokemonSaveEditor.Libraries.RamOffSet;
using PokemonSaveEditor.Libraries.Text;

namespace PokemonSaveEditor.Libraries.Utils.DataHandling
{
    public static class PlayerNameManager
    {
        /// <summary>
        /// Change the player's name
        /// </summary>
        /// <param name="name"></param>
        /// <param name="save"></param>
        /// <returns>The save with the new player's name inside</returns>
        /// <exception cref="ArgumentException">Name is empty or longer than 7 characters</exception>
        public static byte[] SetPlayerName(string name, byte[] save)
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
        /// Returns the player's name stored in save
        /// </summary>
        /// <param name="save"></param>
        /// <returns>The player's name</returns>
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

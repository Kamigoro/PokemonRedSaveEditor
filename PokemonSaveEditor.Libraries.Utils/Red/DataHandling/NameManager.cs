using PokemonSaveEditor.Libraries.Utils.Red.RamOffSet;
using PokemonSaveEditor.Libraries.Utils.Red.Text;

namespace PokemonSaveEditor.Libraries.Utils.Red.DataHandling
{
    /// <summary>
    /// Provides functionality for setting and retrieving the player's name or the rival name in a game save file.
    /// </summary>
    public static class NameManager
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
            return SetName(ref save, name, PlayerNameRamOffset.Start, PlayerNameRamOffset.End);
        }

        /// <summary>
        /// Returns the player's name stored in a save file.
        /// </summary>
        /// <param name="save">The byte array representing the save file to read.</param>
        /// <returns>The player's name as a string.</returns>
        public static string GetPlayerName(byte[] save)
        {
            return GetName(save, PlayerNameRamOffset.Start, PlayerNameRamOffset.End);
        }

        /// <summary>
        /// Change the rival's name in a save file.
        /// </summary>
        /// <param name="save"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static byte[] SetRivalName(ref byte[] save, string name)
        {
            return SetName(ref save, name, RivalNameRamOffset.Start, RivalNameRamOffset.End);
        }

        /// <summary>
        /// Returns the rival's name stored in a save file.
        /// </summary>
        /// <param name="save">The byte array representing the save file to read.</param>
        /// <returns>The rival's name as a string.</returns>
        public static string GetRivalName(byte[] save)
        {
            return GetName(save, RivalNameRamOffset.Start, RivalNameRamOffset.End);
        }

        private static byte[] SetName(ref byte[] save, string name, int ramOffsetStart, int ramOffsetEnd) 
        {
            ValidateName(name);
            var nameByteArray = GetByteArray(name);
            for (int i = ramOffsetStart; i < ramOffsetEnd; i++)
            {
                save[i] = nameByteArray[i - ramOffsetStart];
            }
            return save;
        }

        /// <summary>
        /// Returns the name stored in a save file.
        /// </summary>
        /// <param name="save"></param>
        /// <param name="ramOffsetStart"></param>
        /// <param name="ramOffsetEnd"></param>
        /// <returns></returns>
        private static string GetName(byte[] save, int ramOffsetStart, int ramOffsetEnd)
        {
            byte[] nameByteArray = new byte[11];
            string name = string.Empty;
            for (int i = ramOffsetStart; i < ramOffsetEnd; i++)
            {
                nameByteArray[i - ramOffsetStart] = save[i];
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

        /// <summary>
        /// Validates the name to be set.
        /// </summary>
        /// <param name="name"></param>
        /// <exception cref="ArgumentException"></exception>
        private static void ValidateName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Name can't be empty.");
            }
            else if (name.Length > 7)
            {
                throw new ArgumentException("Name is longer than 7 characters.");
            }
        }   

        /// <summary>
        /// Returns the byte array corresponding to the name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
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

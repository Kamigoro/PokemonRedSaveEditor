using PokemonSaveEditor.Libraries.RamOffSet;
using PokemonSaveEditor.Libraries.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonSaveEditor.Libraries.Utils.DataModification
{
    public static class PlayerNameManager
    {
        /// <summary>
        /// Change the player's name
        /// </summary>
        /// <param name="name"></param>
        /// <param name="ram"></param>
        /// <returns>The ram with the new player's name inside</returns>
        /// <exception cref="ArgumentException">Name is empty or longer than 7 characters</exception>
        public static byte[] SetPlayerName(string name, byte[] ram)
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
                ram[i] = nameByteArray[i-PlayerNameRamOffset.Start];
            }

            return ram;
        }

        /// <summary>
        /// Returns the player's name stored in ram
        /// </summary>
        /// <param name="ram"></param>
        /// <returns>The player's name</returns>
        public static string GetPlayerName(byte[] ram)
        {
            byte[] nameByteArray = new byte[11];
            string name = string.Empty;
            for (int i = PlayerNameRamOffset.Start; i < PlayerNameRamOffset.End; i++)
            {
                nameByteArray[i - PlayerNameRamOffset.Start] = ram[i];
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

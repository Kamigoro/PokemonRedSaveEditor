using PokemonSaveEditor.Libraries.Utils.Red.Models;
using PokemonSaveEditor.Libraries.Utils.Red.RamOffSet;

namespace PokemonSaveEditor.Libraries.Utils.Red.DataHandling
{
    /// <summary>
    /// Provides methods for working with badge collections in a save file.
    /// </summary>
    public static class BadgesManager
    {
        /// <summary>
        /// Sets the specified badge collection in the save file.
        /// </summary>
        /// <param name="save">The save file.</param>
        /// <param name="badgeCollection">The badge collection to set in the save file.</param>
        /// <returns>The updated save file.</returns>
        public static byte[] SetBadgesCollection(ref byte[] save, BadgeCollection badgeCollection)
        {
            save[BadgesRamOffset.Start] = ConvertBadgeCollectionToByte(badgeCollection);
            return save;
        }

        /// <summary>
        /// Returns the badge collection stored in the save file.
        /// </summary>
        /// <param name="save">The save file.</param>
        /// <returns>The badge collection stored in the save file.</returns>
        public static BadgeCollection GetBadgesCollection(byte[] save)
        {
            var badgesCollectionByte = save[BadgesRamOffset.Start];
            return ConvertByteToBadgeCollection(badgesCollectionByte);
        }

        /// <summary>
        /// Takes all the badges and converts them to a single byte.
        /// </summary>
        /// <param name="badgeCollection">The badge collection to convert to a byte.</param>
        /// <returns>A byte representing all the badges earned.</returns>
        private static byte ConvertBadgeCollectionToByte(BadgeCollection badgeCollection)
        {
            var boulderByte = BitConverter.GetBytes(Convert.ToByte(badgeCollection.Boulder) << 7)[0];
            var cascadeByte = BitConverter.GetBytes(Convert.ToByte(badgeCollection.Cascade) << 6)[0];
            var thunderByte = BitConverter.GetBytes(Convert.ToByte(badgeCollection.Thunder) << 5)[0];
            var rainbowByte = BitConverter.GetBytes(Convert.ToByte(badgeCollection.Rainbow) << 4)[0];
            var soulByte = BitConverter.GetBytes(Convert.ToByte(badgeCollection.Soul) << 3)[0];
            var marshByte = BitConverter.GetBytes(Convert.ToByte(badgeCollection.Marsh) << 2)[0];
            var volcanoByte = BitConverter.GetBytes(Convert.ToByte(badgeCollection.Volcano) << 1)[0];
            var earthByte = Convert.ToByte(badgeCollection.Earth);
            var finalByte = BitConverter.GetBytes(boulderByte | cascadeByte | thunderByte | soulByte | rainbowByte | rainbowByte | marshByte | volcanoByte | earthByte)[0];
            return finalByte;
        }

        /// <summary>
        /// Converts the byte containing the badges into a BadgeCollection.
        /// </summary>
        /// <param name="badgeCollectionByte">The byte containing the badges.</param>
        /// <returns>The badge collection corresponding to the byte.</returns>
        private static BadgeCollection ConvertByteToBadgeCollection(byte badgeCollectionByte)
        {
            var badges = new bool[8];

            for (int i = 0; i < 8; i++)
            {
                var shiftedByte = BitConverter.GetBytes(badgeCollectionByte << i)[0];
                badges[i] = shiftedByte / 128 > 0;
            }

            var badgeCollection = new BadgeCollection
            {
                Boulder = badges[0],
                Cascade = badges[1],
                Thunder = badges[2],
                Rainbow = badges[3],
                Soul = badges[4],
                Marsh = badges[5],
                Volcano = badges[6],
                Earth = badges[7],
            };

            return badgeCollection;
        }
    }
}

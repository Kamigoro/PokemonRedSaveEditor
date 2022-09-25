using PokemonSaveEditor.Libraries.RamOffSet.PlayTime;

namespace PokemonSaveEditor.Libraries.Utils.DataModification
{
    public static class PlayTimeManager
    {
        /// <summary>
        /// Set hours and minutes played
        /// </summary>
        /// <param name="ram"></param>
        /// <param name="hours"></param>
        /// <param name="minutes"></param>
        /// <returns>Ram with the newly set hours and minutes</returns>
        /// <exception cref="ArgumentOutOfRangeException">Hours is not between 0 and 255 or minutes are not between 0 and 59</exception>
        public static byte[] SetPlayTime(byte[] ram,int hours, int minutes)
        {
            if (hours < 0 || hours > 255)
            {
                throw new ArgumentOutOfRangeException("Hours played should be between 0 and 255");
            }
            else if(minutes < 0 || minutes > 59)
            {
                throw new ArgumentOutOfRangeException("Minutes played should be between 0 and 59");
            }

            var hoursByte = BitConverter.GetBytes(hours)[0];
            var minutesByte = BitConverter.GetBytes(minutes)[0];

            ram[HoursRamOffset.Start] = hoursByte;
            ram[MinutesRamOffset.Start] = minutesByte;

            return ram;
        }

        /// <summary>
        /// Get the hours and minutes played
        /// </summary>
        /// <param name="ram"></param>
        /// <returns>Time played hours and minutes</returns>
        public static (int, int) GetPlayTime(byte[] ram)
        {
            var hoursByte = ram[HoursRamOffset.Start];
            var minutesByte = ram[MinutesRamOffset.Start];

            return (hoursByte, minutesByte);
        } 
    }
}

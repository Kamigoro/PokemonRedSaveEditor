using PokemonSaveEditor.Libraries.RamOffSet.PlayTime;

namespace PokemonSaveEditor.Libraries.Utils.DataHandling
{
    public static class PlayTimeManager
    {
        /// <summary>
        /// Set hours and minutes played
        /// </summary>
        /// <param name="save"></param>
        /// <param name="hours"></param>
        /// <param name="minutes"></param>
        /// <returns>save with the newly set hours and minutes</returns>
        /// <exception cref="ArgumentOutOfRangeException">Hours is not between 0 and 255 or minutes are not between 0 and 59</exception>
        public static byte[] SetPlayTime(ref byte[] save,int hours, int minutes)
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

            save[HoursRamOffset.Start] = hoursByte;
            save[MinutesRamOffset.Start] = minutesByte;

            return save;
        }

        /// <summary>
        /// Get the hours and minutes played
        /// </summary>
        /// <param name="save"></param>
        /// <returns>Time played hours and minutes</returns>
        public static (int, int) GetPlayTime(byte[] save)
        {
            var hoursByte = save[HoursRamOffset.Start];
            var minutesByte = save[MinutesRamOffset.Start];

            return (hoursByte, minutesByte);
        } 
    }
}

using PokemonSaveEditor.Api.Contracts.Red.Responses;
using PokemonSaveEditor.Libraries.Utils.Red.DataHandling;

namespace PokemonSaveEditor.Api.Contracts.Red.Extensions
{
    public static class SaveFileExtensions
    {
        public static SaveFileDataContent ToSaveFileData(this byte[] data)
        {
            var (hours, minutes) = PlayTimeManager.GetPlayTime(data);

            return new SaveFileDataContent
            {
                PlayerName = NameManager.GetPlayerName(data),
                Badges = BadgesManager.GetBadgesCollection(data),
                Hours = hours,
                Minutes = minutes,
                Money = MoneyManager.GetMoney(data)
            };
        }
    }
}

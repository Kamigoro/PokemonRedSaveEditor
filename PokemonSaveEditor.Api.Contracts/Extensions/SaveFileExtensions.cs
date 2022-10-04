using PokemonSaveEditor.Api.Contracts.Responses;
using PokemonSaveEditor.Libraries.Utils.DataHandling;

namespace PokemonSaveEditor.Api.Contracts.Extensions
{
    public static class SaveFileExtensions
    {
        public static SaveFileDataResponse ToSaveFileData(this byte[] data)
        {
            var (hours, minutes) = PlayTimeManager.GetPlayTime(data);

            return new SaveFileDataResponse
            {
                PlayerName = PlayerNameManager.GetPlayerName(data),
                Badges = BadgesManager.GetBadgesCollection(data),
                Hours = hours,
                Minutes = minutes,
                Money = MoneyManager.GetMoney(data)
            };
        }
    }
}

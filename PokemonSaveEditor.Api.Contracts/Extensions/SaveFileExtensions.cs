using PokemonSaveEditor.Api.Contracts.Inputs;
using PokemonSaveEditor.Api.Contracts.Responses;
using PokemonSaveEditor.Libraries.Utils.DataHandling;

namespace PokemonSaveEditor.Api.Contracts.Extensions
{
    public static class SaveFileExtensions
    {
        public static SaveFileData ToSaveFileData(this SaveFile saveFile)
        {
            var data = saveFile.Data;
            var (hours, minutes) = PlayTimeManager.GetPlayTime(data);

            return new SaveFileData
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

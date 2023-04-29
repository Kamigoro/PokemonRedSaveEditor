using PokemonSaveEditor.Libraries.Utils.Red.Models;

namespace PokemonSaveEditor.Api.Contracts.Red.Responses
{
    public class SaveFileDataContent
    {
        public string PlayerName { get; set; }
        public int Money { get; set; }
        public int Hours { get; set; }
        public int Minutes { get; set; }
        public BadgeCollection Badges { get; set; }
    }
}

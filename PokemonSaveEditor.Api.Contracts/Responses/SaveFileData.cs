using PokemonSaveEditor.Libraries.Models;

namespace PokemonSaveEditor.Api.Contracts.Responses
{
    public class SaveFileData
    {
        public string PlayerName { get; set; }
        public int Money { get; set; }
        public int Hours { get; set; }
        public int Minutes { get; set; }
        public BadgeCollection Badges { get; set; }
    }
}

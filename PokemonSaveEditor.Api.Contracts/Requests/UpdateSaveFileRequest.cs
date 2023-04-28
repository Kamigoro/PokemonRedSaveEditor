using PokemonSaveEditor.Api.Contracts.Responses;
using System.ComponentModel.DataAnnotations;

namespace PokemonSaveEditor.Api.Contracts.Requests
{
    public class UpdateSaveFileRequest
    {
        /// <summary>
        /// The data the player want to be applied to his save file
        /// </summary>
        [Required]
        public SaveFileDataContent NewData { get; set; }

        /// <summary>
        /// Data regarding the save file
        /// </summary>
        [Required]
        public SaveFile SaveFile { get; set; }
    }
}

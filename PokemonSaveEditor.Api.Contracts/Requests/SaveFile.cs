using System.ComponentModel.DataAnnotations;

namespace PokemonSaveEditor.Api.Contracts.Requests
{
    public class SaveFile
    {
        /// <summary>
        /// Name of the save file
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// The save file content as a string
        /// </summary>
        [Required]
        public string Content { get; set; }
    }
}

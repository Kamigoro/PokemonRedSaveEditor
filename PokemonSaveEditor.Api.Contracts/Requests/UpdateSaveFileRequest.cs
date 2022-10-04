using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PokemonSaveEditor.Api.Contracts.Responses;
using PokemonSaveEditor.Api.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace PokemonSaveEditor.Api.Contracts.Requests
{
    [ModelBinder(typeof(JsonWithFilesFormDataModelBinder), Name = "json")]
    public class UpdateSaveFileRequest
    {
        [Required]
        public SaveFileDataResponse NewData { get; set; }
        [Required]
        public IFormFile SaveFile { get; set; }
    }
}

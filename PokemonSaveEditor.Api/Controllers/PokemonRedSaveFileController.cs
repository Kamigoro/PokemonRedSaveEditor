using Microsoft.AspNetCore.Mvc;
using PokemonSaveEditor.Api.Contracts.Extensions;
using PokemonSaveEditor.Api.Contracts.Inputs;
using PokemonSaveEditor.Api.Contracts.Responses;
using PokemonSaveEditor.Api.Routes;

namespace PokemonSaveEditor.Api.Controllers
{
    [Route(PokemonRedRoutes.Base)]
    [ApiController]
    public class PokemonRedSaveFileController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetSaveFileDataAsync(SaveFile saveFile)
        {
            SaveFileData saveFileData = null;
            try
            {
                await Task.Run(() => saveFileData = saveFile.ToSaveFileData());
                return Ok(saveFileData);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}

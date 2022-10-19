using Microsoft.AspNetCore.Mvc;
using PokemonSaveEditor.Api.Contracts.Extensions;
using PokemonSaveEditor.Api.Contracts.Requests;
using PokemonSaveEditor.Api.Routes;
using PokemonSaveEditor.Libraries.Utils;
using PokemonSaveEditor.Libraries.Utils.DataHandling;
using System.Text;

namespace PokemonSaveEditor.Api.Controllers
{
    [Route(PokemonRedRoutes.Base)]
    [ApiController]
    public class PokemonRedSaveFileController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetSaveFileDataAsync(SaveFile saveFile)
        {
            //Extract bytes from save file content
            var saveFileBytes = Encoding.UTF8.GetBytes(saveFile.Content);

            //Checks on the save file
            var (success, errorMessage) = FileHandler.ValidateSaveFile(saveFileBytes);
            if (!success)
            {
                return BadRequest(errorMessage);
            }

            //Converting file into byte array and extracting data
            try
            {
                return Ok(saveFileBytes.ToSaveFileData());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }            
        }

        [HttpPost]
        public async Task<IActionResult> SetNewFileDataAsync(UpdateSaveFileRequest updateSaveFileRequest)
        {
            //Extract bytes from save file content
            var saveFileBytes = Encoding.UTF8.GetBytes(updateSaveFileRequest.SaveFile.Content);

            //Checks on the save file
            var (success, errorMessage) = FileHandler.ValidateSaveFile(saveFileBytes);
            if (!success)
            {
                return BadRequest(errorMessage);
            }

            ///TODO Refactor this in a single method
            saveFileBytes = BadgesManager.SetBadgesCollection(saveFileBytes, updateSaveFileRequest.NewData.Badges);
            saveFileBytes = PlayerNameManager.SetPlayerName(saveFileBytes, updateSaveFileRequest.NewData.PlayerName);
            saveFileBytes = PlayTimeManager.SetPlayTime(saveFileBytes, updateSaveFileRequest.NewData.Hours, updateSaveFileRequest.NewData.Minutes);
            saveFileBytes = MoneyManager.SetMoney(saveFileBytes, updateSaveFileRequest.NewData.Money);
            var newChecksum = RamChecksum.CalculateChecksum(saveFileBytes);
            saveFileBytes = RamChecksum.SetRamCheckSum(newChecksum, saveFileBytes);

            return File(saveFileBytes, "application/octet-stream", updateSaveFileRequest.SaveFile.Name);
        }
    }
}

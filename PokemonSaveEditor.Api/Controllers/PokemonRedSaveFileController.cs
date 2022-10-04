using Microsoft.AspNetCore.Mvc;
using PokemonSaveEditor.Api.Contracts.Extensions;
using PokemonSaveEditor.Api.Contracts.Requests;
using PokemonSaveEditor.Api.Routes;
using PokemonSaveEditor.Libraries.Utils;
using PokemonSaveEditor.Libraries.Utils.DataHandling;

namespace PokemonSaveEditor.Api.Controllers
{
    [Route(PokemonRedRoutes.Base)]
    [ApiController]
    public class PokemonRedSaveFileController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetSaveFileDataAsync(IFormFile file)
        {
            //Checks on the save file
            var (success, errorMessage) = FileHandler.ValidateSaveFile(file);
            if (!success)
            {
                return BadRequest(errorMessage);
            }

            //Converting file into byte array and extracting data
            try
            {
                using (var filestream = file.OpenReadStream())
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await filestream.CopyToAsync(memoryStream);
                        var saveFileBytes = memoryStream.ToArray();
                        return Ok(saveFileBytes.ToSaveFileData());
                    }
                }
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }            
        }

        [HttpPost]
        public async Task<IActionResult> SetNewFileDataAsync(UpdateSaveFileRequest updateSaveFileRequest)
        {
            //Checks on the save file
            var (success, errorMessage) = FileHandler.ValidateSaveFile(updateSaveFileRequest.SaveFile);
            if (!success)
            {
                return BadRequest(errorMessage);
            }

            //Extract bytes from save file
            byte[] saveFileBytes;
            var newData = updateSaveFileRequest.NewData;
            using (var filestream = updateSaveFileRequest.SaveFile.OpenReadStream())
            {
                using (var memoryStream = new MemoryStream())
                {
                    await filestream.CopyToAsync(memoryStream);
                    saveFileBytes = memoryStream.ToArray();
                }
            }

            ///TODO Refactor this in a single method
            saveFileBytes = BadgesManager.SetBadgesCollection(saveFileBytes, newData.Badges);
            saveFileBytes = PlayerNameManager.SetPlayerName(saveFileBytes, newData.PlayerName);
            saveFileBytes = PlayTimeManager.SetPlayTime(saveFileBytes, newData.Hours, newData.Minutes);
            saveFileBytes = MoneyManager.SetMoney(saveFileBytes, newData.Money);
            var newChecksum = RamChecksum.CalculateChecksum(saveFileBytes);
            saveFileBytes = RamChecksum.SetRamCheckSum(newChecksum, saveFileBytes);

            return File(saveFileBytes, "application/octet-stream", updateSaveFileRequest.SaveFile.FileName);
        }
    }
}

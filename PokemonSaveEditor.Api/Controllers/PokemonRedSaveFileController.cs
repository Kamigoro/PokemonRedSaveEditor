using Microsoft.AspNetCore.Mvc;
using PokemonSaveEditor.Api.Contracts.Red.Extensions;
using PokemonSaveEditor.Api.Contracts.Red.Requests;
using PokemonSaveEditor.Api.Routes;
using PokemonSaveEditor.Libraries.RamOffSet;
using PokemonSaveEditor.Libraries.Utils.Red.DataHandling;
using PokemonSaveEditor.Libraries.Utils.Red.FileHandling;
using PokemonSaveEditor.Libraries.Utils.Red.RamHandling;
using System.Text;

namespace PokemonSaveEditor.Api.Controllers
{
    /// <summary>
    /// Controller for managing Pokemon Red save files.
    /// </summary>
    [Route(PokemonRedApiRoutes.Base)]
    [ApiController]
    public class PokemonRedSaveFileController : ControllerBase
    {
        [HttpPost]
        [Route("test")]
        public IActionResult Test(SaveFile saveFile)
        {
            //Extract bytes from save file content
            var saveFileBytes = Convert.FromBase64String(saveFile.Content);

            for (int pokemonNumber = 1; pokemonNumber < 100; pokemonNumber++)
            {
                PokemonSeenManager.SetSightState(ref saveFileBytes, pokemonNumber);
                PokemonOwnedManager.SetOwnedState(ref saveFileBytes, pokemonNumber);
            }

            RamChecksum.SetRamCheckSum(ref saveFileBytes, RamChecksum.CalculateChecksum(saveFileBytes));
            return File(saveFileBytes, "application/octet-stream", saveFile.Name);
        }

        /// <summary>
        /// Returns the data extracted from a Pokemon Red save file.
        /// </summary>
        /// <param name="saveFile">The save file object containing the save file content.</param>
        /// <returns>The extracted save file data.</returns>
        [HttpGet]
        public IActionResult GetSaveFileData(SaveFile saveFile)
        {
            //Extract bytes from save file content
            var saveFileBytes = Convert.FromBase64String(saveFile.Content);

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

        /// <summary>
        /// Updates a Pokemon Red save file with new data.
        /// </summary>
        /// <param name="updateSaveFileRequest">The request object containing the save file and new data.</param>
        /// <returns>The updated save file.</returns>
        [HttpPut]
        public IActionResult UpdateSaveFile(UpdateSaveFileRequest updateSaveFileRequest)
        {
            //Extract bytes from save file content
            var saveFileBytes = Convert.FromBase64String(updateSaveFileRequest.SaveFile.Content);

            //Checks on the save file
            var (success, errorMessage) = FileHandler.ValidateSaveFile(saveFileBytes);
            if (!success)
            {
                return BadRequest(errorMessage);
            }

            SetNewFileData(updateSaveFileRequest, ref saveFileBytes);

            return File(saveFileBytes, "application/octet-stream", updateSaveFileRequest.SaveFile.Name);
        }

        /// <summary>
        /// Sets new data in a Pokemon Red save file based on the update request.
        /// </summary>
        /// <param name="updateSaveFileRequest">The update request object containing the new data to apply to the save file.</param>
        /// <param name="saveFileBytes">A reference to the byte array containing the save file data.</param>
        private static void SetNewFileData(UpdateSaveFileRequest updateSaveFileRequest, ref byte[] saveFileBytes)
        {
            BadgesManager.SetBadgesCollection(ref saveFileBytes, updateSaveFileRequest.NewData.Badges);
            NameManager.SetPlayerName(ref saveFileBytes, updateSaveFileRequest.NewData.PlayerName);
            PlayTimeManager.SetPlayTime(ref saveFileBytes, updateSaveFileRequest.NewData.Hours, updateSaveFileRequest.NewData.Minutes);
            MoneyManager.SetMoney(ref saveFileBytes, updateSaveFileRequest.NewData.Money);
            RamChecksum.SetRamCheckSum(ref saveFileBytes, RamChecksum.CalculateChecksum(saveFileBytes));
        }
    }
}

namespace PokemonSaveEditor.Api.Contracts.Inputs
{
    public class SaveFile
    {
        /// <summary>
        /// Name of the save file
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Save file data
        /// </summary>
        public byte[] Data { get; set; }
    }
}
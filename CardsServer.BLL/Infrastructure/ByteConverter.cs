using Microsoft.AspNetCore.Http;

namespace CardsServer.BLL.Infrastructure
{
    public class ByteConverter
    {
        /// <summary>
        /// Converts IFormFileCollection to List<byte[]>
        /// </summary>
        /// <param name="files">The collection of files</param>
        /// <returns>List of byte arrays</returns>
        public async Task<List<byte[]>> ConvertFilesToByteArrayAsync(IFormFileCollection files)
        {
            List<byte[]> fileBytesList = [];

            foreach (var file in files)
            {
                if (file.Length > 0) // Ensure the file is not empty
                {
                    using var memoryStream = new MemoryStream();
                    await file.CopyToAsync(memoryStream); // Copy the file to memory stream
                    fileBytesList.Add(memoryStream.ToArray()); // Convert memory stream to byte array
                }
            }

            return fileBytesList;
        }
    }
}

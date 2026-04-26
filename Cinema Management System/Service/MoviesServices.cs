namespace Cinema_Management_System.Service
{
    public enum TypeImage
    {
        MainImage = 1,
        SubImage = 2,
    }

    public class MoviesServices
    {
        /// <summary>
        /// Saves the provided image file to the server in a directory based on the specified image type and returns the
        /// generated file name.
        /// </summary>
        /// <remarks>The file is saved in a subdirectory of 'wwwroot/images/admin_layout' based on the
        /// value of typeImage. The file name is generated using the current date and a GUID to ensure
        /// uniqueness.</remarks>
        /// <param name="logo">The image file to be saved. Must not be null and should contain a valid file name and content.</param>
        /// <param name="typeImage">Specifies the category of the image, determining the target directory for saving the file.</param>
        /// <returns>The generated file name of the saved image if the operation succeeds; otherwise, null if an error occurs.</returns>
        public string? SaveImg(IFormFile logo, TypeImage typeImage)
        {
            try
            {
                string filePath;
                var fileName = $"{DateTime.Now.ToString("dd_MM_yyyy")}_{Guid.NewGuid()}{Path.GetExtension(logo.FileName)}";
                //string fileName;
                if (typeImage == TypeImage.MainImage)
                {
                    filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\admin_layout", fileName);

                }
                else if (typeImage == TypeImage.SubImage)
                {
                    filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\admin_layout\\Admin_Subimage", fileName);

                }
                else
                    filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\admin_layout", fileName);

                


                using (var stream = System.IO.File.Create(filePath))
                {
                    logo.CopyTo(stream);
                }

                return fileName;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }
        /// <summary>
        /// Removes an image file from the server based on the specified image name and type.
        /// </summary>
        /// <remarks>The method attempts to delete the specified image file from the appropriate directory under
        /// 'wwwroot/images/admin_layout'. If the file does not exist, the method returns true. If an error occurs during
        /// deletion, the method returns false.</remarks>
        /// <param name="logoName">The name of the image file to remove. This should include the file extension. Cannot be null or empty.</param>
        /// <param name="typeImage">The type of image to remove, which determines the subdirectory where the image is located.</param>
        /// <returns>true if the image file is successfully removed or does not exist; otherwise, false.</returns>
        public bool RemoveImg(string logoName, TypeImage typeImage)
        {
            try

            {
                string filePath;
                if (typeImage == TypeImage.MainImage)
                {
                    filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\admin_layout", logoName);
                }
                else if (typeImage == TypeImage.SubImage)
                {
                    filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\admin_layout\\Admin_Subimage", logoName);

                }
                else
                    filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\admin_layout", logoName);

                if (System.IO.File.Exists(filePath))
                    System.IO.File.Delete(filePath);

                Console.WriteLine($"Remove old img from wwwroot");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }
    }
}

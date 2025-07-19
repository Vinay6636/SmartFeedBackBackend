namespace SmartFeedBack.Helpers
{
    public static class FileUploadHelper
    {
        public static async Task<string?> SaveFileAsync(IFormFile? file)
        {
            if (file == null) return null;

            var folder = Path.Combine("wwwroot", "uploads");
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            var fileName = $"{Guid.NewGuid()}_{file.FileName}";
            var path = Path.Combine(folder, fileName);

            using (var stream = new FileStream(path, FileMode.Create))
                await file.CopyToAsync(stream);

            // Return relative path for database storage
            return $"/uploads/{fileName}";
        }
    }
}

namespace blog.ui.Utility
{
    using System.IO;
    using Microsoft.AspNetCore.Http;
    using System;
    public class FileSystemFileUploader : IFileUpload
    {
        private readonly string _filePath;
        // Kullanıcı farklı bir lokasyon seçerse
        public FileSystemFileUploader(string filePath)
        {
            this._filePath = filePath;
        }

        // default resmin kayıt lokasyonu
        public FileSystemFileUploader()
        {
            this._filePath = "images";
        }
        public FileUploadResult Upload(IFormFile file)
        {
            FileUploadResult result = new FileUploadResult();
            result.FileResult = FileResult.Error;
            result.Message = "Dosya yüklenmesi sırasına hata meydana geldi!";

            if (file.Length > 0)
            {
                // .jpg  / .png
                var fileName = $"{Guid.NewGuid()}{System.IO.Path.GetExtension(file.FileName)}";
                result.OriginalName = file.FileName;
                var phsycalPath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/{_filePath}", fileName);

                if (File.Exists(phsycalPath))
                {
                    result.Message = "Dizin içerisinde böyle bir dosya mevcuttur.";
                }
                else
                {
                    result.FileUrl = $"/{_filePath}/{fileName}";
                    result.Base64 = null;
                    try
                    {
                        using var stream = new FileStream(phsycalPath, FileMode.Create);
                        file.CopyTo(stream);
                        result.Message = "Dosya başarıyla kaydedildi!";
                        result.FileResult = FileResult.Succeded;
                    }
                    catch
                    {
                        result.Message = "Dosya kayıt işlemi sırasında hata meydana geldi!";
                        result.FileResult = FileResult.Error;
                    }
                }
            }
            return result;
        }
    }
}
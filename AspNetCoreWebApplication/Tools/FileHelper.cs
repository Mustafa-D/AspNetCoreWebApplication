namespace AspNetCoreWebApplication.Tools
{
    public class FileHelper
    {
        public static async Task<string> FileLoaderAsync(IFormFile formfile,string klasorYolu="/wwwroot/Img/")
        {
            string dosyaAdi = "";

            if (formfile != null && formfile.Length>0)
            {
                dosyaAdi = formfile.FileName;
                string dizin = Directory.GetCurrentDirectory() + klasorYolu + dosyaAdi;
                using var stream = new FileStream(dizin,FileMode.Create);
                await formfile.CopyToAsync(stream);


            }
            return dosyaAdi;
        }
        public static bool FileRemover(string fileName,string klasorYolu ="/wwwroot/Img/")
        {
            string dizin = Directory.GetCurrentDirectory() + klasorYolu + fileName;
            if (File.Exists(dizin))
            {
                File.Delete(dizin);
                return true;

            }
            return false;
        }
    }
}

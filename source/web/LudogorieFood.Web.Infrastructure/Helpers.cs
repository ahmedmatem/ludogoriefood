namespace LudogorieFood.Web.Infrastructure
{
    using System;
    using System.Web;

    using LudogorieFood.Models;
    using Models.Types;

    public static class Helpers
    {
        public static string GetFileTypeFromName(string fileName)
        {
            var splitedFileName = fileName.Split(new[] { '.' });

            return splitedFileName[splitedFileName.Length - 1];
        }

        public static PictureType ConvertToPictureType(this string fileType)
        {
            switch (fileType)
            {
                case "jpeg": return PictureType.jpg;
                case "jpg": return PictureType.jpg;
                case "png": return PictureType.png;
                case "gif": return PictureType.gif;
            }

            throw new ArgumentException();
        }

        public static string CreateUniqueFileName(string fullFileName)
        {
            var splitedFullFileName = fullFileName.Split(new[] { '.' });

            var fileName = splitedFullFileName[splitedFullFileName.Length - 2];
            var uniqueFileName = fileName + DateTime.Now.Millisecond;

            return uniqueFileName;
        }

        public static string GetBaseUrl(HttpRequestBase request)
        {
            string baseUrl = request.Url.Scheme + "://" + request.Url.Authority +
            request.ApplicationPath.TrimEnd('/') + "/";

            return baseUrl;
        }
    }
}

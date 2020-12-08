using Microsoft.Extensions.Configuration;
using Sample.SharedKernal.Files.Dto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Sample.SharedKernal.Files
{
    public class FileOperation
    {
        private readonly IConfiguration _configuration;
        public FileOperation(IConfiguration config)
        {
            _configuration = config;
        }
        public string SaveFile(FileDto file)
        {
            string correspondenceFolderPath = _configuration.GetSection("FilesPaths").GetSection("BaseUrl").Value;
            string fileName = $"{file.Name}";

            string fileFullPath = $"{correspondenceFolderPath}{fileName}.png";
            byte[] imgByteArray = Convert.FromBase64String(file.File.Split(',')[1]);
            
            File.WriteAllBytes(Path.Combine(fileFullPath), imgByteArray);

            return fileName;
        }
        public bool DeleteFile(string fileName)
        {
            string correspondenceFolderPath = _configuration.GetSection("FilesPaths").GetSection("BaseUrl").Value;
            string fileFullPath = $"{correspondenceFolderPath}{fileName}";
            try
            {
                File.Delete(fileFullPath);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public string ReadFile(string fileName)
        {
            string correspondenceFolderPath = _configuration.GetSection("FilesPaths").GetSection("BaseUrl").Value;
            string fileFullPath = $"{correspondenceFolderPath}{fileName}";
            try
            {
                Byte[] bytes = File.ReadAllBytes(fileFullPath);
                String fileContents = Convert.ToBase64String(bytes);
                return fileContents;
            }
            catch(Exception)
            {
                return "";
            }
        }
    }
}

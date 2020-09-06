using Newtonsoft.Json;
using System.ComponentModel;
using System.IO;
using ToReadApp.Models;

namespace ToReadApp.Services
{
    class FileIOService
    {
        private readonly string PATH;

        public FileIOService(string path)
        {
            PATH = path;
        }

        public BindingList<ToReadModel> LoadData()
        {
            var fileExists = File.Exists(PATH);
            if (!fileExists)
            {
                File.CreateText(PATH).Dispose();
                return new BindingList<ToReadModel>();
            }
            using (var reader = File.OpenText(PATH))
            {
                var fileText = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<BindingList<ToReadModel>>(fileText);
            }
        }
        public void SaveData(object _ToReadBookList)
        {
            using (StreamWriter writer = File.CreateText(PATH))
            {
                string output = JsonConvert.SerializeObject(_ToReadBookList);
                writer.Write(output);
            }
        }
    }
}

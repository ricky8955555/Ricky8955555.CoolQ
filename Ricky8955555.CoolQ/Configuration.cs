using Newtonsoft.Json.Linq;
using System.IO;
using static HuajiTech.CoolQ.CurrentPluginContext;

namespace Ricky8955555.CoolQ
{
    internal abstract partial class Configuration
    {
        internal abstract string Name { get; }

        protected abstract JToken InitInfo { get; }

        internal JToken Config { get; private set; }

        private readonly DirectoryInfo DataDirInfo = Bot.AppDirectory;

        private readonly string Suffix = ".json";

        internal Configuration()
        {
            CreateFile(Name + Suffix);
            if (!WriteToConfig(Read(Name + Suffix)))
                Logger.LogError(Resources.ConfigurationLoading, string.Format(Resources.ConfigurationCannotBeLoaded, Name));
        }

        internal void SetValueAndSave(JToken jToken)
        {
            Config = jToken;
            Save();
        }

        internal void Save()
        {
            Write(Name + Suffix, Config.ToString());
        }

        internal bool Reload()
        {
            string fileName = Name + Suffix;
            if (CreateFile(fileName))
                return WriteToConfig(Read(fileName));
            else
            {
                Config = InitInfo;
                return true;
            }
        }

        internal void Rebuild()
        {
            Write(Name + Suffix, InitInfo.ToString());
            Config = InitInfo;
        }

        private void Write(string fileName, string content)
        {
            string filePath = DataDirInfo.FullName + "\\" + fileName;
            File.WriteAllText(filePath, content);
        }

        private string Read(string fileName)
        {
            var file = new FileInfo(DataDirInfo.FullName + "\\" + fileName);

            if (file.Exists)
            {
                string fileContent = File.ReadAllText(file.FullName);
                return fileContent;
            }
            else
                return null;
        }

        private bool CreateFile(string fileName)
        {
            if (File.Exists(DataDirInfo.FullName + "\\" + fileName))
                return true;
            else
            {
                Write(fileName, InitInfo.ToString());
                Config = InitInfo;
                return false;
            }
        }

        private bool WriteToConfig(string content)
        {
            try
            {
                Config = JToken.Parse(content);
                return true;
            }
            catch
            {
                Config = null;
                return false;
            }
        }
    }
}
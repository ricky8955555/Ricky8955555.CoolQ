using System.IO;
using Newtonsoft.Json.Linq;
using static HuajiTech.CoolQ.CurrentPluginContext;

namespace Ricky8955555.CoolQ
{
    abstract class Configuration
    {
        public abstract string Name { get; }
        protected abstract JToken InitInfo { get; }
        public JToken Config { get; private set; }

        readonly DirectoryInfo DataDirInfo = Bot.AppDirectory;
        readonly string Suffix = ".json";

        public Configuration()
        {
            CreateFile(Name + Suffix);
            if (!WriteToConfig(Read(Name + Suffix)))
                Logger.LogError(Resources.ConfigurationLoading, string.Format(Resources.ConfigurationCannotBeLoaded, Name));
        }

        public void SetValueAndSave(JToken jToken)
        {
            Config = jToken;
            Save();
        }

        public void Save()
        {
            Write(Name + Suffix, Config.ToString());
        }

        public bool Reload()
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

        public void Rebuild()
        {
            Write(Name + Suffix, InitInfo.ToString());
            Config = InitInfo;
        }

        void Write(string fileName, string content)
        {
            string filePath = DataDirInfo.FullName + "\\" + fileName;
            File.WriteAllText(filePath, content);
        }

        string Read(string fileName)
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

        bool CreateFile(string fileName)
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

        bool WriteToConfig(string content)
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
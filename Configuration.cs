using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static HuajiTech.CoolQ.CurrentPluginContext;

namespace Ricky8955555.CoolQ
{
    abstract class Configuration
    {
        public abstract string Name { get; }
        protected abstract JToken InitInfo { get; }
        public JToken Config { get; private set; }

        readonly DirectoryInfo DataDirInfo = Bot.DataDirectory;
        readonly string Suffix = ".json";

        public Configuration()
        {
            Init();
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

        void Init()
        {
#if DEBUG
            Logger.LogDebug("配置加载", $"开始加载配置（{Name}）");
            if (CreateFile(Name + Suffix))
                Logger.LogDebug("配置加载", $"文件 {Name}.json 已存在，将不对其进行改动（{Name}）");
            else
                Logger.LogDebug("配置加载", $"文件 {Name}.json 不存在，将会新建（{Name}）");

            string fileContent = Read(Name + Suffix);
            if (fileContent != null)
                Logger.LogDebug("配置加载", $"加载配置成功（{Name}）");
            else
                Logger.LogError("配置加载", $"加载配置失败（{Name}）");

            if (WriteToConfig(fileContent))
                Logger.LogDebug("配置加载", $"解析配置成功（{Name}）");
            else
                Logger.LogError("配置加载", $"解析配置失败（{Name}）");
#else
            CreateFile(Name + Suffix);
            if (!WriteToConfig(Read(Name + Suffix)))
                Logger.LogError("配置加载", $"解析配置失败（{Name}）");
#endif
        }
    }
}
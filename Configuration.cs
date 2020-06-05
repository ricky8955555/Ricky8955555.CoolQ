using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using HuajiTech.CoolQ;

namespace Ricky8955555.CoolQ
{
    abstract class Configuration
    {
        public abstract string Name { get; }
        public abstract JObject InitInfo { get; }
        public JObject Config { get; set; }

        readonly DirectoryInfo DataDirInfo = PluginContext.Current.Bot.DataDirectory;
        readonly string Suffix = ".json";

        public Configuration()
        {
            Init();
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
            System.IO.File.WriteAllText(filePath, content);
        }

        string Read(string fileName)
        {
            var file = new FileInfo(DataDirInfo.FullName + "\\" + fileName);

            if (file.Exists)
            {
                string fileContent = System.IO.File.ReadAllText(file.FullName);
                return fileContent;
            }
            else
                return null;
        }

        bool CreateFile(string fileName)
        {
            if (System.IO.File.Exists(DataDirInfo.FullName + "\\" + fileName))
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
                Config = JObject.Parse(content);
                return true;
            }
            catch
            {
                Config = null;
                return false;
            }
        }

        void Init(bool debugMode = false)
        {
            var logger = Main.XLogger;

#if DEBUG
            logger.LogDebug("配置加载", $"开始加载配置（{Name}）");
            if (CreateFile(Name + Suffix))
                logger.LogDebug("配置加载", $"文件 {Name}.json 已存在，将不对其进行改动（{Name}）");
            else
                logger.LogDebug("配置加载", $"文件 {Name}.json 不存在，将会新建（{Name}）");

            string fileContent = Read(Name + Suffix);
            if (fileContent != null)
                logger.LogDebug("配置加载", $"加载配置成功（{Name}）");
            else
                logger.LogError("配置加载", $"加载配置失败（{Name}）");

            if (WriteToConfig(fileContent))
                logger.LogDebug("配置加载", $"解析配置成功（{Name}）");
            else
                logger.LogError("配置加载", $"解析配置失败（{Name}）");
#else
            CreateFile(Name + Suffix);
            if (!WriteToConfig(Read(Name + Suffix)))
                logger.LogError("配置加载", $"解析配置失败（{Name}）");
#endif
        }
    }
}
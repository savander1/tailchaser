using System;
using System.IO;
using System.Security.AccessControl;
using System.Threading;
using System.Xml;
using Newtonsoft.Json;
using TailChaser.Entity;
using TailChaser.Entity.Configuration;
using TailChaser.UI.Exceptions;
using TailChaser.UI.ViewModels;
using File = System.IO.File;

namespace TailChaser.UI.Loaders
{
    public class ConfigLoader
    {
        private const string ConfigFileName = ".init.cfg";
        private static readonly string ConfigDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                                                                    "TailChaser");

        private static readonly string ConfigFullPath = Path.Combine(ConfigDirectory, ConfigFileName);

        public MainWindowViewModel LoadConfiguration()
        {
            var config = new MainWindowViewModel();
            try
            {
                using (var stream = new FileStream(ConfigFullPath, FileMode.Open, FileSystemRights.Read | FileSystemRights.Modify, FileShare.ReadWrite, 8, FileOptions.WriteThrough, new FileSecurity(ConfigFullPath, AccessControlSections.All)) ) // File.Open(ConfigFullPath, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    using (var reader = new StreamReader(stream))
                    {
                        var container = JsonConvert.DeserializeObject<Container>(reader.ReadToEnd());
                        config = ConfigConverter.ConvertToViewModel(container);
                        return config;
                    }
                }
            }
            catch (FileNotFoundException)
            {
                SaveConfiguration(config, true);
                return config;
            }

            catch (DirectoryNotFoundException)
            {
                SaveConfiguration(config);
                return config;
            }
            catch (XmlException)
            {
                SaveConfiguration(config);
                return config;
            }
            catch (Exception ex)
            {
                throw new InvalidConfigurationException(ex);
            }
        }

        public void SaveConfiguration(MainWindowViewModel config, bool setAtributes = false)
        {
            try
            {
                var container = ConfigConverter.ConvertToEntity(config);
                using (
                    var stream = new FileStream(ConfigFullPath, FileMode.CreateNew, FileAccess.ReadWrite,
                                                FileShare.ReadWrite))
                {
                    using (var writer = new StreamWriter(stream))
                    {
                        writer.Write(JsonConvert.SerializeObject(container));
                    }
                }
                if (setAtributes)
                {
                    File.SetAttributes(ConfigFullPath, FileAttributes.Hidden | FileAttributes.NotContentIndexed);
                }
            }
            catch (ArgumentException)
            {
                Thread.Sleep(TimeSpan.FromMilliseconds(100));
                SaveConfiguration(config, setAtributes);
            }
            catch (DirectoryNotFoundException)
            {
                Directory.CreateDirectory(ConfigDirectory);
                SaveConfiguration(config, setAtributes);
            }
            catch (Exception ex)
            {
                throw new InvalidConfigurationException(ex);
            }
        }
    }
}

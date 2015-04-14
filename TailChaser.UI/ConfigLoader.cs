using System;
using System.IO;
using System.Security.AccessControl;
using System.Xml;
using System.Xml.Serialization;
using TailChaser.Entity;
using TailChaser.Entity.Configuration;
using TailChaser.UI.Exceptions;

namespace TailChaser.UI
{
    public class ConfigLoader
    {
        private const string ConfigFileName = ".init.cfg";
        private static readonly string ConfigDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                                                                    "TailChaser");

        private static readonly string ConfigFullPath = Path.Combine(ConfigDirectory, ConfigFileName);

        public Configurations LoadConfiguration()
        {
            var config = new Configurations();
            try
            {
                var serializer = new XmlSerializer(typeof(Configurations));
                using (var stream = new FileStream(ConfigFullPath, FileMode.Open, FileSystemRights.Read | FileSystemRights.Modify, FileShare.ReadWrite, 8, FileOptions.WriteThrough, new FileSecurity(ConfigFullPath, AccessControlSections.All)) ) // File.Open(ConfigFullPath, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    return serializer.Deserialize(stream) as Configurations;
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

        public void SaveConfiguration(Configurations config, bool setAtributes = false)
        {
            try
            {
                var serializer = new XmlSerializer(typeof(Configurations));
                using (
                    var stream = new FileStream(ConfigFullPath, FileMode.OpenOrCreate, FileAccess.ReadWrite,
                                                FileShare.ReadWrite))
                {
                    serializer.Serialize(stream, config);
                }
                if (setAtributes)
                {
                    File.SetAttributes(ConfigFullPath, FileAttributes.Hidden | FileAttributes.NotContentIndexed);
                }
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

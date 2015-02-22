using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using TailChaser.Entity;
using TailChaser.Exceptions;

namespace TailChaser.Code
{
    public class ConfigLoader
    {
        private const string ConfigFileName = ".init.cfg";
        private static readonly string ConfigDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                                                                    "TailChaser");

        private static readonly string ConfigFullPath = Path.Combine(ConfigDirectory, ConfigFileName);

        public Configuration LoadConfiguration()
        {
            var config = new Configuration();
            try
            {
                var serializer = new XmlSerializer(typeof(Configuration));
                using (var stream = File.Open(ConfigFullPath, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    return serializer.Deserialize(stream) as Configuration;
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

        public void SaveConfiguration(Configuration config, bool setAtributes = false)
        {
            try
            {
                var serializer = new XmlSerializer(typeof(Configuration));
                using (
                    var stream = new FileStream(ConfigFullPath, FileMode.Create, FileAccess.ReadWrite,
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

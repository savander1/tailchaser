using System;
using System.IO;
using System.Xml;
using TailChaser.Entity;
using TailChaser.Exceptions;

namespace TailChaser.Code
{
    public class ConfigLoader
    {
        private const string ConfigFileName = "data.cfg";
        private static readonly string ConfigDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                                                                    "TailChaser");

        private static readonly string ConfigFullPath = Path.Combine(ConfigDirectory, ConfigFileName);

        public Configuration LoadConfiguration()
        {
            var config = new Configuration();
            try
            {
                using (var stream = File.Open(ConfigFullPath, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    var bytes = new byte[stream.Length];
                    stream.Read(bytes, 0, bytes.Length);
                    stream.Flush();

                    config = Configuration.Deserialize(bytes);

                    return config;
                }
            }
            catch (FileNotFoundException)
            {
                SaveConfiguration(config);
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

        public void SaveConfiguration(Configuration config)
        {
            try
            {
                var configBytes = config.Serialize();

                using (
                    var stream = new FileStream(ConfigFullPath, FileMode.OpenOrCreate, FileAccess.ReadWrite,
                                                FileShare.ReadWrite))
                {
                    stream.Write(configBytes, 0, configBytes.Length);
                    stream.Flush(true);
                }
            }
            catch (DirectoryNotFoundException)
            {
                Directory.CreateDirectory(ConfigDirectory);
                SaveConfiguration(config);
            }
            catch (Exception ex)
            {
                throw new InvalidConfigurationException(ex);
            }
        }
    }
}

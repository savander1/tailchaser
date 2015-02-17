using System;
using System.IO;
using System.Security.AccessControl;
using TailChaser.Entity;
using TailChaser.Exceptions;

namespace TailChaser.Code
{
    public class ConfigLoader
    {
        private const string ConfigLocation = @"C:\ProgramData\TailChaser\data.cfg";
        private const string ConfigDirectory = @"C:\ProgramData\TailChaser\";

        public Configuration LoadConfiguration()
        {
            var config = new Configuration();
            try
            {
                using (var stream = File.Open(ConfigLocation, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
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
                    var stream = new FileStream(ConfigLocation, FileMode.OpenOrCreate, FileAccess.ReadWrite,
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

using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace TailChaser.Entity
{
    public class Configuration
    {
        public List<Machine> Machines { get; set; } 

        public Configuration()
        {
            Machines = new List<Machine>();
        }

        public byte[] Serialize()
        {
            var doc = ConfigDocument();
            var buffLen = Encoding.UTF8.GetByteCount(doc.InnerXml) + 1024;
            var buffer = new byte[buffLen];
            using (var stream = new MemoryStream(buffer))
            {
                
                doc.Save(stream);
                stream.Flush();
            }
            return buffer;
        }

        private XmlDocument ConfigDocument()
        {
            var doc = new XmlDocument();
            var dec = doc.CreateXmlDeclaration("1.0", "utf-8", null);
            doc.AppendChild(dec);

            var root = doc.CreateElement("machines");
            foreach (var machine in Machines)
            {
                var machineElm = doc.CreateElement("machine");
                machineElm.SetAttribute("name", machine.Name);
                foreach (var group in machine.Groups)
                {
                    var grpElm = doc.CreateElement("group");
                    grpElm.SetAttribute("name", group.Name);
                    var files = doc.CreateElement("files");
                    foreach (var file in group.Files)
                    {
                        var fileElm = doc.CreateElement("file");
                        fileElm.SetAttribute("name", file.Name);
                        fileElm.SetAttribute("path", file.FullName);
                        files.AppendChild(fileElm);
                    }
                    grpElm.AppendChild(files);
                    machineElm.AppendChild(grpElm);
                }
                root.AppendChild(machineElm);
            }
            doc.AppendChild(root);

            return doc;
        }

        public static Configuration Deserialize(byte[] bytes)
        {
            using (var stream = new MemoryStream(bytes))
            {
                var doc = new XmlDocument();
                doc.Load(stream);

                return GetConfig(doc);
            }
        }

        private static Configuration GetConfig(XmlDocument doc)
        {
            var config = new Configuration();
            using (var reader = new XmlNodeReader(doc))
            {
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element && reader.LocalName == "machine")
                    {

                        var machineName = reader.GetAttribute("name");
                        var machine = new Machine(machineName);
                        while (reader.ReadToDescendant("group"))
                        {
                            var name = reader.GetAttribute("name");
                            var group = new Group(name);
                            while (reader.ReadToDescendant("file"))
                            {
                                var fileName = reader.GetAttribute("name");
                                var filePath = reader.GetAttribute("path");
                                group.Files.Add(new TailedFile(fileName, filePath));
                            }
                            machine.Groups.Add(group);
                        }
                        config.Machines.Add(machine);
                    }
                }
            }
            return config;
        }

        public override string ToString()
        {
           return ConfigDocument().InnerXml;
        }
    }
}

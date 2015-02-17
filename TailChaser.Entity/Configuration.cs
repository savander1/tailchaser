using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace TailChaser.Entity
{
    public class Configuration
    {
        private const string EncryptionKey =
            "3098CD2785CE176C559F745189F06204BE6AA526652B96DFFB8CB2AA3E2247E171CB5CBBA4BAA16486A2AA700332B4591CD92257C041EF75D8EDBE32F6171964";


        public List<Group> Groups { get; set; } 

        public Configuration()
        {
            Groups = new List<Group>();
        }

        public byte[] Serialize()
        {
            var doc = ConfigDocument();
            var buffLen = doc.InnerXml.Length + 1024;
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

            var root = doc.CreateElement("groups");
            foreach (var group in Groups)
            {
                var grpElm = doc.CreateElement("group");
                var name = doc.CreateNode(XmlNodeType.Element, "name", null);
                var nameNode = doc.CreateTextNode(group.Name);
                name.AppendChild(nameNode);

                grpElm.AppendChild(name);

                var files = doc.CreateElement("files");
                foreach (var file in group.Files)
                {
                    var fileElm = doc.CreateElement("file");
                    fileElm.SetAttribute("name", file.Name);
                    fileElm.SetAttribute("path", file.FullName);
                    files.AppendChild(fileElm);
                }
                grpElm.AppendChild(files);
                root.AppendChild(grpElm);
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
                    if (reader.NodeType == XmlNodeType.Element && reader.LocalName == "group")
                    {
                        reader.ReadToDescendant("name");
                        var name = reader.ReadElementContentAsString();
                        var group = new Group(name);
                        while (reader.ReadToDescendant("file"))
                        {
                            var fileName = reader.GetAttribute("name");
                            var filePath = reader.GetAttribute("path");
                            group.Files.Add(new TailedFile(fileName, filePath));
                        }
                        config.Groups.Add(group);
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

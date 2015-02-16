using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TailChaser.Entity
{
    [DataContract]
    [Serializable]
    public class Configuration
    {
        [DataMember]
        public List<Group> Groups { get; set; } 

        public byte[] Serialize()
        {
            var doc = GetConfig();
            var buffLen = doc.InnerXml.Length + 1024;
            var buffer = new byte[buffLen];
            using (var stream = new MemoryStream(buffer))
            {
                
                doc.Save(stream);
                stream.Flush();
            }
            return buffer;
        }

        private XmlDocument GetConfig()
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

                foreach (var file in group.Files)
                {
                    var fileElm = doc.CreateElement("file");
                    fileElm.SetAttribute("name", file.Name);
                    var path = doc.CreateNode(XmlNodeType.Element, "path", null);
                    var pathNode = doc.CreateTextNode(file.FullName);
                    path.AppendChild(pathNode);

                    fileElm.AppendChild(path);

                    grpElm.AppendChild(fileElm);
                }
                root.AppendChild(grpElm);
            }
            doc.AppendChild(root);

            return doc;
        }
    }
}

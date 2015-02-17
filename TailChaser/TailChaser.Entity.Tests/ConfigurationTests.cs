using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TailChaser.Entity.Tests
{
    [TestClass]
    public class ConfigurationTests
    {
        private static readonly Configuration Configuration = new Configuration
        {
            Groups = new List<Group>
                        {
                            new Group
                                {
                                    Name = "First",
                                    Files = new List<TailedFile>
                                        {
                                            new TailedFile("WebServices.log", "C:\\Logs\\WebServices.log")
                                        }
                                }
                        }
        };

        [TestMethod]
        public void Serialize()
        {
            //arrange

            // act
            var result = Configuration.Serialize();

            //assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Length > 0);
        }

        [TestMethod]
        public void Deserialize()
        {
            // arrange
            var bytes = Configuration.Serialize();

            // act
            var result = Configuration.Deserialize(bytes);

            // assert
            Assert.IsNotNull(result);
            Assert.AreEqual(Configuration.Groups.Count, result.Groups.Count);
            Assert.AreEqual(Configuration.Groups[0].Name, result.Groups[0].Name);
            Assert.AreEqual(Configuration.Groups[0].Files.Count, result.Groups[0].Files.Count);
            Assert.AreEqual(Configuration.Groups[0].Files[0].Name, result.Groups[0].Files[0].Name);
            Assert.AreEqual(Configuration.Groups[0].Files[0].FullName, result.Groups[0].Files[0].FullName);
        }
    }
}

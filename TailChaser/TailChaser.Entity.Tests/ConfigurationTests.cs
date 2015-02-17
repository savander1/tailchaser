
using System.Collections.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TailChaser.Entity.Tests
{
    [TestClass]
    public class ConfigurationTests
    {
        private static readonly Configuration Configuration = new Configuration
            {
                Machines = new ObservableCollection<Machine>
                    {
                        new Machine
                            {
                                Name = "machine",
                                Groups = new ObservableCollection<Group>
                                    {
                                        new Group
                                            {
                                                Name = "First",
                                                Files = new ObservableCollection<TailedFile>
                                                    {
                                                        new TailedFile("WebServices.log", "C:\\Logs\\WebServices.log")
                                                    }
                                            }
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
            Assert.AreEqual(Configuration.Machines.Count, result.Machines.Count);
            Assert.AreEqual(Configuration.Machines[0].Name, result.Machines[0].Name);
            Assert.AreEqual(Configuration.Machines[0].Groups.Count, result.Machines[0].Groups.Count);
            Assert.AreEqual(Configuration.Machines[0].Groups[0].Name, result.Machines[0].Groups[0].Name);
            Assert.AreEqual(Configuration.Machines[0].Groups[0].Files.Count, result.Machines[0].Groups[0].Files.Count);
            Assert.AreEqual(Configuration.Machines[0].Groups[0].Files[0].Name, result.Machines[0].Groups[0].Files[0].Name);
            Assert.AreEqual(Configuration.Machines[0].Groups[0].Files[0].FullName, result.Machines[0].Groups[0].Files[0].FullName);
        }
    }
}

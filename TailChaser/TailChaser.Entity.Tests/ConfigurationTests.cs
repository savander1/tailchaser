using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TailChaser.Entity.Tests
{
    [TestClass]
    public class ConfigurationTests
    {
        [TestMethod]
        public void Serialize()
        {
            //arrange
            var config = new Configuration
                {
                    Groups = new List<Group>
                        {
                            new Group
                                {
                                    Name = "First",
                                    Files = new List<FileInfo>
                                        {
                                            new FileInfo("C:\\Logs\\WebServices.log")
                                        }
                                }
                        }
                };

            // act
            var result = config.Serialize();

            //assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Length > 0);
        }
    }
}

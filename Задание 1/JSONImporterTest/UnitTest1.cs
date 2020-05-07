using Microsoft.VisualStudio.TestTools.UnitTesting;
using JSONImporter;
using System.IO;
using BenchmarkDotNet.Running;

namespace JSONImporterTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CreateJsonTest()
        {
            var t = JSONImporter.Program.method.CreateJson();
            Assert.AreEqual(true, t);
            
        }

        [TestMethod]
        public void ImportJsonTest()
        {
            var list = JSONImporter.Program.method.ImportJson();
            Assert.AreNotEqual(0,list.Count);
        }
    }
}

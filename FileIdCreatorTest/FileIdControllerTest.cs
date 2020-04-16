using FileIdCreator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace FileIdCreatorTest {
    [TestClass]
    public class FileIdControllerTest {

        const string pathInit = "TestFile/dummy.pdf";
        const string path = "TestFile/dummyTest.pdf";

        [TestInitialize]
        public void init() {           
            File.Copy(pathInit, path);
        }

        [TestCleanup]
        public void cleanUp() {
            File.Delete(path);
        }

        [TestMethod]
        public void deleteIDTest() {
            var fileID = new FileIdController();
            fileID.addID(path, 1);
            var actualId = fileID.getID(path);
            Assert.AreEqual("###0000000001###", actualId);

            fileID.deleteID(path);
            var shouldBeNull = fileID.getID(path);
            Assert.IsNull(shouldBeNull);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void exceptionCheck() {
            var fileID = new FileIdController();
            var nullPath = "";
            fileID.addID(nullPath, 1);
        }

        [TestMethod]
        public void updateIDTest() {
            var fileID = new FileIdController();
            fileID.updateID(path, 123);
            var actualId = fileID.getID(path);
            Assert.AreEqual("###0000000123###", actualId);         
        }

    }
}

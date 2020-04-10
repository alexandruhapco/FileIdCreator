using FileIdCreator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;

namespace FileIdCreatorTest {
    [TestClass]
    public class FileIdTest {

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
            FileId.addID(path, 1);
            var actualId = FileId.getID(path);
            Assert.AreEqual("###0000000001###", actualId);

            FileId.deleteID(path);
            var shouldBeNull = FileId.getID(path);
            Assert.IsNull(shouldBeNull);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void exceptionCheck() {
            var nullPath = "";
            FileId.addID(nullPath, 1);
        }

        [TestMethod]
        public void updateIDTest() {          
            FileId.updateID(path, 123);
            var actualId = FileId.getID(path);
            Assert.AreEqual("###0000000123###", actualId);         
        }

    }
}

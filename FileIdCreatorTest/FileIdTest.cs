using FileIdCreator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace FileIdCreatorTest {
    [TestClass]
    public class FileIdTest {

        const string path = "";

        [TestMethod]
        public void deleteIDTest() {           
            FileId.addID(path, 1);
            var a = FileId.getID(path);
            Assert.AreEqual("###0000000001###", a);

            FileId.deleteID(path);
            var b = FileId.getID(path);
            Assert.IsNull(b);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void exceptionCheck() {
            var nullPath = "";
            FileId.addID(nullPath, 1);
        }

    }
}

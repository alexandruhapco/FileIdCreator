using FileIdCreator;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FileIdCreatorTest {
    [TestClass]
    public class FileIdTest {
        [TestMethod]
        public void deleteIDTest() {
            const string path = "C:/Users/E160396/OneDrive - S.C. Johnson & Son, Inc/Desktop/pdf attachments/KPT 2926606.pdf";

            FileId.addID(path, 1);
            var a = FileId.getID(path);
            Assert.AreEqual("###0000000001###", a);

            FileId.deleteID(path);
            var b = FileId.getID(path);
            Assert.IsNull(b);
        }
    }
}

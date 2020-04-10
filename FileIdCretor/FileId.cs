using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileIdCreator
{
    public class FileId {

        public static void addID(string path, long id) {
            string strID = $"###{id.ToString().PadLeft(10, '0')}###";
            byte[] b = Encoding.ASCII.GetBytes(strID);
            appendAllBytes(path, b);
        }

        public static string getID(string path) {
            using (var reader = new StreamReader(path)) {
                if (reader.BaseStream.Length > 16) {
                    reader.BaseStream.Seek(-16, SeekOrigin.End);
                }
                string line = reader.ReadLine();

                if (isID(line)) {
                    return line;
                }
                return null;
            }
        }

        private static bool isID(string id) {
            return id != null && id.Length == 16 && id.StartsWith("###") && id.EndsWith("###") && id.Substring(3, 10).ToList().TrueForAll((x) => int.TryParse(x.ToString(), out int num));
        }

        private static void appendAllBytes(string path, byte[] bytes) {         

            using (var stream = new FileStream(path, FileMode.Append)) {
                stream.Write(bytes, 0, bytes.Length);
            }
        }
    }
}

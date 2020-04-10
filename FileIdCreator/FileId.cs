using System;
using System.IO;
using System.Linq;
using System.Text;

namespace FileIdCreator {
    public class FileId {

        public static void addID(string path, long id) {
            Guard.throwIfStringNullOrEmpty(path,nameof(path));

            string strID = $"###{id.ToString().PadLeft(10, '0')}###";
            byte[] b = Encoding.ASCII.GetBytes(strID);
            appendAllBytes(path, b);
        }

        public static string getID(string path) {
            Guard.throwIfStringNullOrEmpty(path, nameof(path));

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

        public static void deleteID(string path) {
            Guard.throwIfStringNullOrEmpty(path, nameof(path));

            while (getID(path) != null) {
                deleteBytes(path, 16);
            }
        }

        public static void updateID(string path, long id) {
            Guard.throwIfStringNullOrEmpty(path, nameof(path));

            deleteID(path);
            addID(path, id);
        }     

        private static bool isID(string id) {
            return id != null && id.Length == 16 && id.StartsWith("###") && id.EndsWith("###") && id.Substring(3, 10).ToList().TrueForAll((x) => int.TryParse(x.ToString(), out int num));
        }

        private static void deleteBytes(string path, int bytesNumber) {
            Guard.throwIfStringNullOrEmpty(path, nameof(path));

            var fi = new FileInfo(path);
            using (var fs = fi.Open(FileMode.Open)) {
                fs.SetLength(Math.Max(0, fi.Length - bytesNumber));
                fs.Close();
            }
        }

        private static void appendAllBytes(string path, byte[] bytes) {
            Guard.throwIfStringNullOrEmpty(path, nameof(path));
            Guard.throwIfNull(bytes, nameof(bytes));

            using (var stream = new FileStream(path, FileMode.Append)) {
                stream.Write(bytes, 0, bytes.Length);
            }
        }
    }
}

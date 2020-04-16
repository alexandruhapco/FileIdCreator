using System;
using System.IO;
using System.Linq;
using System.Text;

namespace FileIdCreator {
    /// <summary>
    /// create/read/update/delete bytes that represent id in the end of the file. 
    /// Id format "###0000000000###"
    /// </summary>
    public class FileIdController : IFileIdController {
      
        /// <summary>
        /// add 16 bytes id in the end of the file
        /// </summary>
        /// <param name="path"></param>
        /// <param name="id"></param>
        public void addID(string path, long id) {
            Guard.throwIfStringNullOrEmpty(path,nameof(path));

            string strID = $"###{id.ToString().PadLeft(10, '0')}###";
            byte[] b = Encoding.ASCII.GetBytes(strID);
            appendAllBytes(path, b);
        }

        /// <summary>
        /// read only last 16 bytes of the file
        /// </summary>
        /// <param name="path"></param>
        /// <returns>id or null if last 16 bytes doesn't meet the pattern of id - ###0000000000###</returns>
        public string getID(string path) {
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

        /// <summary>
        /// deleting last 16 bytes of the file while they meet the pattern of id - ###0000000000###
        /// </summary>
        /// <param name="path"></param>
        public void deleteID(string path) {
            Guard.throwIfStringNullOrEmpty(path, nameof(path));

            while (getID(path) != null) {
                deleteBytes(path, 16);
            }
        }

        /// <summary>
        /// deleting last 16 bytes of the file while they meet the pattern of id - ###0000000000###,
        /// add 16 bytes id in the end of the file
        /// </summary>
        /// <param name="path"></param>
        public void updateID(string path, long id) {
            Guard.throwIfStringNullOrEmpty(path, nameof(path));

            deleteID(path);
            addID(path, id);
        }     

        private bool isID(string id) {
            return id != null && id.Length == 16 && id.StartsWith("###") && id.EndsWith("###") && id.Substring(3, 10).ToList().TrueForAll((x) => int.TryParse(x.ToString(), out int num));
        }

        /// <summary>
        /// delete bytes from the end of the file
        /// </summary>
        /// <param name="path"></param>
        /// <param name="bytesNumber">number of bytes to delete</param>
        private void deleteBytes(string path, int bytesNumber) {
            Guard.throwIfStringNullOrEmpty(path, nameof(path));

            var fi = new FileInfo(path);
            using (var fs = fi.Open(FileMode.Open)) {
                fs.SetLength(Math.Max(0, fi.Length - bytesNumber));
                fs.Close();
            }
        }

        /// <summary>
        /// append bytes in the end of the file
        /// </summary>
        /// <param name="path"></param>
        /// <param name="bytes"></param>
        private void appendAllBytes(string path, byte[] bytes) {
            Guard.throwIfStringNullOrEmpty(path, nameof(path));
            Guard.throwIfNull(bytes, nameof(bytes));

            using (var stream = new FileStream(path, FileMode.Append)) {
                stream.Write(bytes, 0, bytes.Length);
            }
        }
       
    }
}

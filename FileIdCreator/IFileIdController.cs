namespace FileIdCreator {
    interface IFileIdController {
        void addID(string path, long id);
        string getID(string path);
        void deleteID(string path);
        void updateID(string path, long id);
    }
}

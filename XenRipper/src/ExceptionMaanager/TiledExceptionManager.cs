using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace XenRipper.src.ExceptionManager {
    static class TiledExceptionManager {
        public class UnreadableFileException : FileNotFoundException {
            public UnreadableFileException() { }

            public UnreadableFileException(string file)
                : base(String.Format("Target filed could not be found: {0}", file)){}
        }

        public class MissingDirectoryException : Exception {
            public MissingDirectoryException() { }

            public MissingDirectoryException(string file)
                : base(String.Format("Target directory could not be found: {0}", file)) { }
        }

        public class CouldntCreateDirectoryException : Exception {
            public CouldntCreateDirectoryException() { }

            public CouldntCreateDirectoryException(string file)
                : base(String.Format("Target directory could not be created: {0}", file)) { }
        }

        public class CouldntSplitTilesCorrectlyException : Exception {
            public CouldntSplitTilesCorrectlyException() { }

            public CouldntSplitTilesCorrectlyException(string file)
                : base(String.Format("Couldnt split image into tiles correctly: {0}", file)) { }
        }

        public class CouldntCopyFilesException : Exception {
            public CouldntCopyFilesException() { }

            public CouldntCopyFilesException(string file)
                : base(String.Format("Couldnt copy file correctly: {0}", file)) { }
        }
    }
}

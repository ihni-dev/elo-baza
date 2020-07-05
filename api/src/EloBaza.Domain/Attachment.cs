using System;

namespace EloBaza.Domain
{
    public class Attachment
    {
        public const int FileSystemMaximumFileName = 256;

        public int Id { get; private set; }
        public string FileName { get; private set; }
        public Uri FileUri { get; private set; }
        public long FileSize { get; private set; }

        public Explanation? Explanation { get; private set; }
        public Question? Question { get; private set; }

        protected Attachment() { }

        internal Attachment(Explanation explanation, string fileName, Uri fileUri, long fileSize)
        {
            Explanation = explanation;

            FileName = fileName;
            FileUri = fileUri;
            FileSize = fileSize;
        }
    }
}
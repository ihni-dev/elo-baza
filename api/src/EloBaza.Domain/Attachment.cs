using System;

namespace EloBaza.Domain
{
    public class Attachment
    {
        public int Id { get; private set; }
        public string FileName { get; private set; }
        public string FileExtension { get; private set; }
        public Uri FileUri { get; private set; }
        public long FileSize { get; private set; }

        public Explanation Explanation { get; private set; }

        internal Attachment(Explanation explanation, string fileName, string fileExtension, Uri fileUri, long fileSize)
        {
            Explanation = explanation;

            FileName = fileName;
            FileExtension = fileExtension;
            FileUri = fileUri;
            FileSize = fileSize;
        }
    }
}
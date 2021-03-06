﻿using EloBaza.Domain.SharedKernel;
using System;

namespace EloBaza.Domain.QuestionAggregate
{
    public class Attachment : Entity
    {
        public const int FileSystemMaximumFileName = 256;

        public string FileName { get; private set; }
        public Uri FileUri { get; private set; }
        public long FileSize { get; private set; }

        public Explanation? Explanation { get; private set; }
        public Question? Question { get; private set; }

        protected Attachment() { }

        internal Attachment(string fileName, Uri fileUri, long fileSize)
        {
            Key = Guid.NewGuid();

            FileName = fileName;
            FileUri = fileUri;
            FileSize = fileSize;
        }
    }
}
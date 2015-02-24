using System;
using TailChaser.Entity;
using TailChaser.Tail;

namespace TailChaser.Code
{
    public class WatchedFileGroup : IDisposable
    {
        private readonly FileTailer _tailer;
        public Guid FileId { get; set; }
        public string FilePath { get; set; }

        public WatchedFileGroup(TailedFile file, FileTailer tailer)
        {
            _tailer = tailer;
            FileId = file.Id;
            FilePath = file.FullName;
            _tailer = tailer;
        }

        protected bool Equals(WatchedFileGroup other)
        {
            return FileId.Equals(other.FileId);
        }

        public override int GetHashCode()
        {
            return FileId.GetHashCode();
        }

        public void Dispose()
        {
            _tailer.Dispose();
        }
        public override bool Equals(object obj)
        {
            var other = obj as WatchedFileGroup;
            if (other == null)
            {
                throw new InvalidOperationException(string.Format("Cannot compare object of type {0} with {1}",
                                                                  obj.GetType(), GetType()));
            }

            return FileId.Equals(other.FileId);
        }
    }
}
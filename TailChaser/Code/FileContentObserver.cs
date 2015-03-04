using System.Collections.Generic;
using DiffMatchPatch;
using TailChaser.Entity;
using TailChaser.Entity.Interfaces;

namespace TailChaser.Code
{
    public class FileContentObserver : IFileContentObserver
    {
        private readonly TailedFile _file;
        
        public FileContentObserver(ref TailedFile file)
        {
            _file = file;
        }

        public void UpdatFileContent(List<Patch> patches)
        {
            var currentContent = _file.FileContent;

            var dmp = new diff_match_patch();
            dmp.patch_apply(patches, currentContent);

            _file.FileContent = currentContent;
        }  
    }
}
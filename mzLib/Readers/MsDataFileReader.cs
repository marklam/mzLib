using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Readers;
using MassSpectrometry;
using MzLibUtil;

namespace Readers
{
    public static class MsDataFileReader
    {
        private static readonly Dictionary<SupportedFileType, Func<string, MsDataFile>> _fileTypeToReaderFactory = new Dictionary<SupportedFileType, Func<string, MsDataFile>>
        {
            { SupportedFileType.MzML, filePath => new Mzml(filePath) },
            { SupportedFileType.Mgf, filePath => new Mgf(filePath) },
            { SupportedFileType.BrukerTimsTof, filePath => new TimsTofFileReader(filePath) },
            { SupportedFileType.Ms1Align, filePath => new Ms1Align(filePath) },
            { SupportedFileType.Ms2Align, filePath => new Ms2Align(filePath) },
        };

        public static void RegisterReader(SupportedFileType fileType, Func<string, MsDataFile> readerFactory)
        {
            _fileTypeToReaderFactory[fileType] = readerFactory;
        }

        public static MsDataFile GetDataFile(string filePath)
        {
            if (!_fileTypeToReaderFactory.TryGetValue(filePath.ParseFileType(), out var readerFactory))
            {
                throw new MzLibException("File type not supported");
            }
            return readerFactory(filePath);
        }
    }
}

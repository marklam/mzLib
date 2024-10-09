using System.Runtime.CompilerServices;

namespace Readers.x64
{
    internal static class RegisterReaders
    {
        [ModuleInitializer]
        internal static void Register()
        {
            MsDataFileReader.RegisterReader(SupportedFileType.ThermoRaw, filePath => new ThermoRawFileReader(filePath));
            MsDataFileReader.RegisterReader(SupportedFileType.BrukerD, filePath => new BrukerFileReader(filePath));
        }
    }

    public static class Initializer
    {
        public static void Initialize()
        {
            // Referencing this method causes the assembly to be loaded.
            // The Module Initializer RegisterReaders.Register will be called when assembly is loaded.
        }
    }
}

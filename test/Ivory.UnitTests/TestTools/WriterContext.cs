using Ivory.Soap;
using System;
using System.IO;
using System.Text;

namespace Ivory.UnitTests.TestTools
{
    public sealed class WriterContext : IDisposable
    {
        private WriterContext(MemoryStream stream, SoapWriterSettings settings)
        {
            Stream = stream;
            Settings = settings;
        }

        public MemoryStream Stream { get; }
        public SoapWriterSettings Settings { get; }

        public string Content => Encoding.UTF8.GetString(Stream.ToArray());

        public static WriterContext Create(SoapWriterSettings settings)
        {
            var stream = new MemoryStream();
            return new WriterContext(stream, settings);
        }

        public void Dispose()
        {
            Stream.Dispose();
        }
    }
}

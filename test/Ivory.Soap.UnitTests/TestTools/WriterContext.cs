using System;
using System.IO;
using System.Text;
using System.Xml;

namespace Ivory.Soap.UnitTests.TestTools
{
    public sealed class WriterContext : IDisposable
    {
        private WriterContext(MemoryStream stream, XmlWriter writer, SoapWriterSettings settings)
        {
            Stream = stream;
            Writer = writer;
            Settings = settings;
        }

        public MemoryStream Stream { get; }
        public XmlWriter Writer { get; }
        public SoapWriterSettings Settings { get; }

        public string Content => Encoding.UTF8.GetString(Stream.ToArray());

        public static WriterContext Create(SoapWriterSettings settings)
        {
            var stream = new MemoryStream();
            return new WriterContext(stream, XmlWriter.Create(stream, settings), settings);
        }

        public void Dispose()
        {
            Writer.Dispose();
            Stream.Dispose();
        }
    }
}

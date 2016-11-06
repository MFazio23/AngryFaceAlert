using System.IO;

namespace AngryFaceAlertApis.Extensions
{
    public static class StreamExtensions
    {
        public static Stream CreateStreamCopy(this Stream stream)
        {
            var memStream = new MemoryStream();

            if(stream.CanSeek) stream.Seek(0, SeekOrigin.Begin);
            stream.CopyTo(memStream);
            if (memStream.CanSeek) memStream.Seek(0, SeekOrigin.Begin);

            return memStream;
        }
    }
}
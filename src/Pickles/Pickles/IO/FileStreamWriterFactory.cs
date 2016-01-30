using System.IO;
using System.Text;

namespace PicklesDoc.Pickles.IO
{
    public class FileStreamWriterFactory : IStreamWriterFactory
    {
        public StreamWriter Create(string filePath)
        {
            return new StreamWriter(filePath, false, Encoding.UTF8);
        }
    }
}
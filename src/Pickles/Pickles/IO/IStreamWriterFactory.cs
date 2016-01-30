using System.IO;

namespace PicklesDoc.Pickles.IO
{
    public interface IStreamWriterFactory
    {
        StreamWriter Create(string filePath);
    }
}

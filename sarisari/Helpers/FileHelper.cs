using System.IO;
using System.Threading.Tasks;

namespace sarisari
{
    public class FileHelper
    {
        public async static Task CopyFile(string source, string destination)
        {
            await Task.Delay(1);
            File.Copy(source, destination);
        }
    }
}

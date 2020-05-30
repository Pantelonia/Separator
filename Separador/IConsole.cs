using System;
namespace Separator
{
    public interface IConsole
    {
        void WriteLine(string message);
        void WriteLine();
        void Write(string message);
        string ReadLine();
        void Clear();
    }
}
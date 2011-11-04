namespace FhemDotNet.Repository.Interfaces
{
    public interface ITelnetConnection
    {
        bool IsConnected { get; }
        string Read();
        void Write(string cmd);
        void WriteLine(string cmd);
    }
}

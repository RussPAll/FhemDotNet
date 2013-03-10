using System.Text;
using System.Net.Sockets;
using FhemDotNet.CrossCutting;
using FhemDotNet.Repository.Interfaces;

namespace FhemDotNet.Repository
{
    enum Verbs
    {
        Will = 251,
        Wont = 252,
        Do = 253,
        Dont = 254,
        Iac = 255
    }

    enum Options
    {
        Sga = 3
    }

    public class TelnetConnection : ITelnetConnection
    {
        readonly TcpClient _tcpSocket;

        const int TimeOutMs = 100;

        public TelnetConnection(string hostname, int portNo)
        {
            Validation.NotNullOrEmpty(() => hostname);
            _tcpSocket = new TcpClient(hostname, portNo);
        }

        public void WriteLine(string cmd)
        {
            Write(cmd + "\n");
        }

        /// <exception cref="System.ArgumentNullException">cmd is not specified</exception>
        public void Write(string cmd)
        {
            if (!_tcpSocket.Connected) return;
            byte[] buf = ASCIIEncoding.ASCII.GetBytes(cmd.Replace("\0xFF", "\0xFF\0xFF"));
            _tcpSocket.GetStream().Write(buf, 0, buf.Length);
        }

        /// <exception cref="System.ArgumentOutOfRangeException">Sleep timeout const is set to an invalid value</exception>
        /// <exception cref="System.ObjectDisposedException">The System.Net.Sockets.Socket has been closed.</exception>
        /// <exception cref="System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information.</exception>
        public string Read()
        {
            if (!_tcpSocket.Connected) return null;
            StringBuilder sb = new StringBuilder();
            do
            {
                ParseTelnet(sb);
                System.Threading.Thread.Sleep(TimeOutMs);
            } while (_tcpSocket.Available > 0);
            return sb.ToString();
        }

        public bool IsConnected
        {
            get { return _tcpSocket.Connected; }
        }

        void ParseTelnet(StringBuilder sb)
        {
            while (_tcpSocket.Available > 0)
            {
                int input = _tcpSocket.GetStream().ReadByte();
                switch (input)
                {
                    case -1:
                        break;
                    case (int)Verbs.Iac:
                        // interpret as command
                        int inputverb = _tcpSocket.GetStream().ReadByte();
                        if (inputverb == -1) break;
                        switch (inputverb)
                        {
                            case (int)Verbs.Iac:
                                //literal Iac = 255 escaped, so append char 255 to string
                                sb.Append(inputverb);
                                break;
                            case (int)Verbs.Do:
                            case (int)Verbs.Dont:
                            case (int)Verbs.Will:
                            case (int)Verbs.Wont:
                                // reply to all commands with "Wont", unless it is Sga (suppres go ahead)
                                int inputoption = _tcpSocket.GetStream().ReadByte();
                                if (inputoption == -1) break;
                                _tcpSocket.GetStream().WriteByte((byte)Verbs.Iac);
                                if (inputoption == (int)Options.Sga)
                                    _tcpSocket.GetStream().WriteByte(inputverb == (int)Verbs.Do ? (byte)Verbs.Will : (byte)Verbs.Do);
                                else
                                    _tcpSocket.GetStream().WriteByte(inputverb == (int)Verbs.Do ? (byte)Verbs.Wont : (byte)Verbs.Dont);
                                _tcpSocket.GetStream().WriteByte((byte)inputoption);
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        sb.Append((char)input);
                        break;
                }
            }
        }
    }
}

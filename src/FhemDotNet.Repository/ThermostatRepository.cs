using System.Collections.Generic;
using System.Linq;
using FhemDotNet.Domain;
using FhemDotNet.Repository.Interfaces;
using FhemDotNet.Repository.Exceptions;
using System.Xml;
using System.Diagnostics;
using FhemDotNet.Repository.Mappers;

namespace FhemDotNet.Repository
{

    public class ThermostatRepository : IThermostatRepository
    {
        private readonly ITelnetConnection _telnetConnection;
        private const int ServerTimeoutMilliseconds = 500;

        public ThermostatRepository(ITelnetConnection telnetConnection)
        {
            _telnetConnection = telnetConnection;
        }

        public IList<Thermostat> GetThermostatList()
        {
            XmlDocument document = GetXmlDocumentFromFhem("xmllist", ServerTimeoutMilliseconds);
            XmlNodeList nodeList = document.SelectNodes("//FHT");

            return (from XmlNode node in nodeList
                    select FhemThermostatMapper.GetThermostatFromFhemEntry(node)).ToList();
        }

        public Thermostat GetThermostatByName(string deviceName)
        {
            return GetThermostatList().FirstOrDefault(x => x.Name == deviceName);
        }

        public void SetThermostatDesiredTemp(float newDesiredTemp)
        {
            throw new System.NotImplementedException();
        }

        private XmlDocument GetXmlDocumentFromFhem(string command, int timeout)
        {
            _telnetConnection.WriteLine(command);

            string response = "";

            Stopwatch timer = Stopwatch.StartNew();
            System.Exception lastException = null;

            while (timer.ElapsedMilliseconds < timeout)
            {
                response += _telnetConnection.Read();
                var document = new XmlDocument();
                try
                {
                    document.LoadXml(response);
                    return document;
                }
                catch (XmlException exception)
                {
                    // Assume that the document's not completely loaded yet, so we'll try again
                    lastException = exception;
                }
            }

            if (string.IsNullOrEmpty(response))
                throw new FhemEmptyResponseException(command);

            throw new FhemResponseTimeoutException(command, timeout, lastException);
        }
    }
}

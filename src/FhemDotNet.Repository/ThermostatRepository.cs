using System.Collections.Generic;
using FhemDotNet.Domain;
using FhemDotNet.Repository.Interfaces;
using FhemDotNet.Repository.Exceptions;
using System.Xml;
using System.Diagnostics;
using System.Globalization;
using System;

namespace FhemDotNet.Repository
{

    public class ThermostatRepository : IThermostatRepository
    {
        private readonly ITelnetConnection _telnetConnection;
        private readonly int _serverTimeoutMilliseconds;
 
        public ThermostatRepository(ITelnetConnection telnetConnection, int serverTimeoutMilliseconds)
        {
            _telnetConnection = telnetConnection;
            _serverTimeoutMilliseconds = serverTimeoutMilliseconds;
        }

        public IList<Thermostat> GetThermostatList()
        {
            List<Thermostat> thermostatList = new List<Thermostat>();

            XmlDocument document = GetXmlDocumentFromFhem("xmllist", _serverTimeoutMilliseconds);
            XmlNodeList nodeList = document.SelectNodes("//FHT");

            foreach (XmlNode node in nodeList)
            {
                XmlNode nameNode = node.SelectSingleNode("./INT[@key='NAME']");
                XmlNode currentStateNode = node.SelectSingleNode("./STATE[@key='measured-temp']");
                XmlNode desiredStateNode = node.SelectSingleNode("./STATE[@key='desired-temp']");
                //XmlNode idNode = node.SelectSingleNode("./INT[@key='DEF']");

                Thermostat thermostat = new Thermostat()
                {
                    Name = nameNode.Attributes["value"].Value,
                    CurrentTemp = currentStateNode == null
                        ? null
                        : ParseFhemTemperature(currentStateNode.Attributes["value"].Value),
                    DesiredTemp = desiredStateNode == null
                        ? null
                        : ParseFhemTemperature(desiredStateNode.Attributes["value"].Value)
                };

                thermostatList.Add(thermostat);
            }

            return thermostatList;
        }

        private float? ParseFhemTemperature(string tempString)
        {
            if (tempString.Contains(" "))
                tempString = tempString.Substring(0, tempString.IndexOf(" "));

            float result;
            return (float.TryParse(tempString, out result))
                ? (float?)result
                : null;
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
                XmlDocument document = new XmlDocument();
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

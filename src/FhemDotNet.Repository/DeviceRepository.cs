using System.Collections.Generic;
using FhemDotNet.Domain;
using FhemDotNet.Repository.Interfaces;
using FhemDotNet.Repository.Exceptions;
using System.Xml;

namespace FhemDotNet.Repository
{

    public class ThermostatRepository : IThermostatRepository
    {
        private readonly ITelnetConnection _telnetConnection;
 
        public ThermostatRepository(ITelnetConnection telnetConnection)
        {
            _telnetConnection = telnetConnection;
        }

        public IList<Thermostat> GetThermostatList()
        {
            List<Thermostat> thermostatList = new List<Thermostat>();

            XmlDocument document = GetXmlDocumentFromFhem("xmllist");
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
                    CurrentTemperature = currentStateNode == null
                        ? "Unavailable"
                        : currentStateNode.Attributes["value"].Value,
                    DesiredTemperature = desiredStateNode == null
                        ? "Unavailable"
                        : desiredStateNode.Attributes["value"].Value
                };

                thermostatList.Add(thermostat);
            }

            return thermostatList;
        }

        private XmlDocument GetXmlDocumentFromFhem(string command)
        {
            string thermostatXml = _telnetConnection.ExecuteAndWaitForResponse(command);
            if (string.IsNullOrEmpty(thermostatXml))
                throw new FhemServerException("FHEM Telnet server has returned a blank response to \"" + command + "\" command.");

            try
            {
                XmlDocument document = new XmlDocument();
                document.LoadXml(thermostatXml);
                return document;
            }
            catch (XmlException exc)
            {
                throw new FhemServerException("An exception occurred querying the FHEM server with command \"list\"", exc);
            }
        }
    }
}

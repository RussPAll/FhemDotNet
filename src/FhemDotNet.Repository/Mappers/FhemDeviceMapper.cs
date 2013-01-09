using System.Xml;
using FhemDotNet.Domain;
using FhemDotNet.Repository.Exceptions;

namespace FhemDotNet.Repository.Mappers
{
    public static class FhemDeviceMapper
    {
        private static class XPathSelectors
        {
            public const string NodeName = "./INT[@key='NAME']";
            public const string NodeMeasuredTemp = "./STATE[@key='measured-temp']";
            public const string NodeDesiredTemp = "./STATE[@key='desired-temp']";
        }

        public static Thermostat GetThermostatFromFhemEntry(XmlNode node)
        {
            XmlNode nameNode = node.SelectSingleNode(XPathSelectors.NodeName);
            XmlNode currentStateNode = node.SelectSingleNode(XPathSelectors.NodeMeasuredTemp);
            XmlNode desiredStateNode = node.SelectSingleNode(XPathSelectors.NodeDesiredTemp);

            if (nameNode == null || nameNode.Attributes == null)
                throw new FhemMalformedResponseException("Cannot find valid node matching selector " + XPathSelectors.NodeName);

            return new Thermostat
            {
                Name = nameNode.Attributes["value"].Value,
                CurrentTemp = GetTemperatureFromNode(currentStateNode),
                DesiredTemp = GetTemperatureFromNode(desiredStateNode)
            };
        }

        private static float? GetTemperatureFromNode(XmlNode fhemNode)
        {
            if (fhemNode == null) return null;

            string tempString = fhemNode.Attributes["value"].Value;
            if (tempString.Contains(" "))
                tempString = tempString.Substring(0, tempString.IndexOf(" "));

            float result;
            return (float.TryParse(tempString, out result))
                       ? (float?)result
                       : null;
        }
    }
}

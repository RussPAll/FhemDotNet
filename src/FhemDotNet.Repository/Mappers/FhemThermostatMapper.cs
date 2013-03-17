using System;
using System.Globalization;
using System.Xml;
using FhemDotNet.Domain;
using FhemDotNet.Repository.Exceptions;

namespace FhemDotNet.Repository.Mappers
{
    public static class FhemThermostatMapper
    {
        private static class XPathSelectors
        {
            public const string NodeName = "./INT[@key='NAME']";
            public const string NodeMeasuredTemp = "./STATE[@key='measured-temp']";
            public const string NodeDesiredTemp = "./STATE[@key='desired-temp']";
            public const string Actuator = "./STATE[@key='actuator']";
        }

        public static Thermostat GetThermostatFromFhemEntry(XmlNode node)
        {
            var nameNode = node.SelectSingleNode(XPathSelectors.NodeName);
            if (nameNode == null || nameNode.Attributes == null)
                throw new FhemMalformedResponseException("Cannot find valid node matching selector " + XPathSelectors.NodeName);

            var thermostat = new Thermostat
                                 {
                                     Name = nameNode.Attributes["value"].Value,
                                     CurrentTemp = GetTemperatureFromNode(node, XPathSelectors.NodeMeasuredTemp),
                                     DesiredTemp = GetTemperatureFromNode(node, XPathSelectors.NodeDesiredTemp),
                                     Actuator = GetValueFromNode(node, XPathSelectors.Actuator),
                                     Mode = NullMeasurement<ThermostatMode>.Instance
                                 };
            GetScheduleFromNode(thermostat.Schedule, node);
            return thermostat;
        }

        private static void GetScheduleFromNode(DaySchedule[] schedule, XmlNode node)
        {
            for (int day = 0; day < 7; day++ )
            {
                for (int period = 0; period < 2; period++)
                {
                    var fromTime = GetFhemPeriodTime(node, day, period, false);
                    var toTime = GetFhemPeriodTime(node, day, period, true);
                    if (fromTime.HasValue && toTime.HasValue)
                    {
                        schedule[day].AddPeriod(
                            new TimePeriod(fromTime.Value, toTime.Value));
                    }
                }
            }
        }

        private static DateTime? GetFhemPeriodTime(XmlNode node, int day, int period, bool isTo)
        {
            string fhemKey = GetFhemPeriodKey(day, period, isTo);
            var fhemNode = GetValueFromNode(node, "./STATE[@key='" + fhemKey + "']");
            string value = fhemNode != null ? fhemNode.Value : null;
            return string.IsNullOrEmpty(value)
                       ? null
                       : (DateTime?) DateTime.Parse(value);
        }

        private static string GetFhemPeriodKey(int day, int period, bool isTo)
        {
            return string.Format("{0}-{1}{2}",
                                 new CultureInfo("en-US").DateTimeFormat.AbbreviatedDayNames[day].ToLower(),
                                 isTo ? "to" : "from",
                                 period + 1);
        }

        private static Measurement<float?> GetTemperatureFromNode(XmlNode fhemNode, string xPathSelector)
        {
            var node = GetValueFromNode(fhemNode, xPathSelector);
            if (node is NullMeasurement<string> || string.IsNullOrEmpty(node.Value))
                return NullMeasurement<float?>.Instance;
            string value = node.Value;

            if (value.Contains(" "))
                value = value.Substring(0, value.IndexOf(" ", StringComparison.Ordinal));

            float result;
            return float.TryParse(value, out result)
                       ? new Measurement<float?>(result, node.Timestamp)
                       : null;
        }

        private static Measurement<string> GetValueFromNode(XmlNode fhemNode, string xPathSelector)
        {
            if (fhemNode == null) return NullMeasurement<string>.Instance;
            var resultNode = fhemNode.SelectSingleNode(xPathSelector);

            if (resultNode == null || resultNode.Attributes == null) return NullMeasurement<string>.Instance;

            return new Measurement<string>(
                resultNode.Attributes["value"].Value,
                DateTime.Parse(resultNode.Attributes["measured"].Value));
        }

    }

    public class NullMeasurement<T> : Measurement<T>
    {
        private static NullMeasurement<T> _instance;

        private NullMeasurement()
        : base(default(T), DateTime.MinValue)
        { }

        public static NullMeasurement<T> Instance 
        {
            get
            {
                if (_instance == null)
                    _instance = new NullMeasurement<T>();
                return _instance;
            }
        }
    }
}

﻿using System;
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
                                     Actuator = GetValueFromNode(node, XPathSelectors.Actuator)
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
            string value = GetValueFromNode(node, "./STATE[@key='" + fhemKey + "']");
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

        private static float? GetTemperatureFromNode(XmlNode fhemNode, string xPathSelector)
        {
            var value = GetValueFromNode(fhemNode, xPathSelector);
            if (string.IsNullOrEmpty(value)) return null;
            
            if (value.Contains(" "))
                value = value.Substring(0, value.IndexOf(" ", StringComparison.Ordinal));

            float result;
            return (float.TryParse(value, out result))
                       ? (float?)result
                       : null;
        }

        private static string GetValueFromNode(XmlNode fhemNode, string xPathSelector)
        {
            if (fhemNode == null) return null;
            var resultNode = fhemNode.SelectSingleNode(xPathSelector);

            if (resultNode == null || resultNode.Attributes == null) return null;

            return resultNode.Attributes["value"].Value;
        }

    }
}

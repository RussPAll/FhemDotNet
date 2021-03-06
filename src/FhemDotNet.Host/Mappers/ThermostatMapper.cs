﻿using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using FhemDotNet.Domain;
using FhemDotNet.Host.Models;

namespace FhemDotNet.Host.Mappers
{
    public class ThermostatMapper
    {
        public static ThermostatViewModel DomainToViewModel(Thermostat input)
        {
            return new ThermostatViewModel
            {
                Name = input.Name,
                Actuator = new MeasurementViewModel<string>(input.Actuator.Value, input.Actuator.Timestamp),
                CurrentTemp = new MeasurementViewModel<string>(input.CurrentTemp.Value.ToString(), input.CurrentTemp.Timestamp),
                DesiredTemp = new MeasurementViewModel<float>(input.DesiredTemp.Value ?? 5, input.DesiredTemp.Timestamp),
                Mode = new MeasurementViewModel<string>(input.Mode.Value == ThermostatMode.Auto ? "Auto" : "Manu", input.Mode.Timestamp),
                DaySchedules = GetDaySchedules(input)
            };
        }

        private static DayScheduleViewModel[] GetDaySchedules(Thermostat input)
        {
            var result = new DayScheduleViewModel[7];
            for (int inputIndex = 0; inputIndex < input.Schedule.Length; inputIndex++)
            {
                var daySchedule = input.Schedule[inputIndex];

                var outputIndex = (inputIndex == 0 ? 6 : inputIndex - 1);
                result[outputIndex] = GetDayScheduleViewModel(inputIndex, daySchedule.Periods.ToList());
            }

            return result;
        }

        private static DayScheduleViewModel GetDayScheduleViewModel(int inputIndex, IList<TimePeriod> periods)
        {
            return new DayScheduleViewModel
                       {
                           Name = new CultureInfo("en-US").DateTimeFormat.DayNames[inputIndex],
                           FromAM = periods.Count > 0 ? periods[0].FromTime.ToString("hh:mm") : string.Empty,
                           ToAM = periods.Count > 0 ? periods[0].ToTime.ToString("hh:mm") : string.Empty,
                           FromPM = periods.Count > 1 ? periods[1].FromTime.ToString("hh:mm") : string.Empty,
                           ToPM = periods.Count > 1 ? periods[1].ToTime.ToString("hh:mm") : string.Empty,
                       };
        }
    }
}
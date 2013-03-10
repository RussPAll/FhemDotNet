using System.Collections.Generic;
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
                Actuator = input.Actuator,
                CurrentTemp = input.CurrentTemp.ToString(),
                DesiredTemp = input.DesiredTemp ?? 5,
                Mode = input.Mode == ThermostatMode.Auto ? "Auto" : "Manu",
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
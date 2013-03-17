﻿using System.Collections.Generic;

namespace FhemDotNet.Domain
{
    public class ThermostatList : List<Thermostat>
    {
        public static ThermostatList Load(IThermostatRepository repository)
        {
            return (ThermostatList)repository.GetThermostatList();
        }
    }

    public interface IThermostatRepository
    {
        List<Thermostat> GetThermostatList();
    }
}

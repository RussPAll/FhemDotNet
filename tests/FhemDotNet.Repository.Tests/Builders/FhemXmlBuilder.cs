namespace FhemDotNet.Repository.Tests.Builders
{
    public static class FhemXmlBuilder
    {
        public static string GetEmptyThermostatList()
        {
            return @"<FHZINFO></FHZINFO>";
        }
        public static string GetThermostatList(int count)
        {
            string result = "<FHZINFO><FHT_LIST>";

            for (int c = 0; c < count; c++)
            {
                result += GetThermostat("thermostat" + c);
            }

            result += "</FHT_LIST></FHZINFO>";
            return result;
        }

        public static string GetThermostat(string deviceName, bool includeTimingData = false)
        {
            string result = @"<FHT name=""" + deviceName + @""" state=""???"" sets=""day day-temp desired-temp fri-from1 fri-from2 fri-to1 fri-to2 holiday1 holiday2 hour lowtemp-offset manu-temp minute mode mon-from1 mon-from2 mon-to1 mon-to2 month night-temp report1 report2 sat-from1 sat-from2 sat-to1 sat-to2 sun-from1 sun-from2 sun-to1 sun-to2 thu-from1 thu-from2 thu-to1 thu-to2 tue-from1 tue-from2 tue-to1 tue-to2 wed-from1 wed-from2 wed-to1 wed-to2 windowopen-temp year"" attrs=""room comment alias IODev do_not_notify:0,1 model;fht80b dummy:0,1 showtime:0,1 loglevel:0,1,2,3,4,5,6 retrycount minfhtbuffer lazy tmpcorr ignore:0,1"">
                        <INT key=""CODE"" value=""3e5c""/>
                        <INT key=""DEF"" value=""3e5c""/>
                        <INT key=""NAME"" value=""" + deviceName + @"""/>
                        <INT key=""NR"" value=""3""/>
                        <INT key=""STATE"" value=""???""/>
                        <INT key=""TYPE"" value=""FHT""/>
                        <ATTR key=""retrycount"" value=""3""/>
                        <STATE key=""actuator"" value=""10"" measured=""2010-10-11 01:10:11""/>
                        <STATE key=""measured-temp"" value=""11.0 (Celsius)"" measured=""2010-10-11 02:20:22""/>
                        <STATE key=""desired-temp"" value=""12.0"" measured=""2010-10-11 03:30:33""/>";
            if (includeTimingData)
                result += "\n" + GetThermostatTimingData();
            return result + "\n</FHT>";
        }

        private static string GetThermostatTimingData()
        {
            return @"<STATE key=""sat-from1"" value=""08:00"" measured=""2010-10-11 20:00:00""/>
<STATE key=""sat-from2"" value=""16:00"" measured=""2010-10-11 20:00:00""/>
<STATE key=""sat-to1"" value=""10:00"" measured=""2010-10-11 20:00:00""/>
<STATE key=""sat-to2"" value=""18:00"" measured=""2010-10-11 20:00:00""/>";
        }
    }
}

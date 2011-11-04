using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Globalization;

namespace FhemDotNet.CrossCutting
{
    public class ConfigHelper
    {
        public static string FhemServerName
        {
            get
            {
                return new ConfigHelper().GetAppSetting("FhemServerName", "");
            }
        }

        public static int FhemServerPort
        {
            get
            {
                return new ConfigHelper().GetIntAppSetting("FhemServerPort", 7072);
            }
        }

        public int GetIntAppSetting(string fieldName, int defaultValue)
        {
            string strResult = GetAppSetting(fieldName, defaultValue.ToString(CultureInfo.InvariantCulture));
            int result;
            bool test;
            test = Int32.TryParse(strResult, NumberStyles.Integer, CultureInfo.CurrentCulture, out result);
            if (!test)
                throw new ConfigurationErrorsException(
                    String.Format(CultureInfo.InvariantCulture, "Unable to convert AppSetting key \"{0}\" value {1} to an integer.",
                    fieldName, strResult)
                );

            return result;
        }

        public virtual string GetAppSetting(string fieldName, string defaultValue)
        {
            if (ConfigurationManager.AppSettings.AllKeys.Contains(fieldName))
            {
                return ConfigurationManager.AppSettings[fieldName];
            }
            else if (!string.IsNullOrEmpty(defaultValue))
            {
                return defaultValue;
            }
            else
            {
                throw new ConfigurationErrorsException(
                    String.Format(CultureInfo.InvariantCulture, "Unable to find AppSetting key \"{0}\" and no default value has been specified.",
                    fieldName)
                );
            }
        }
    }
}

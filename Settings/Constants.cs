using System;
using System.Diagnostics;

namespace Utilities.Settings
{
    /// <summary>
    /// This is just how i like to do my CONSTs, strongly typed, organized, explicit
    /// </summary>
    public static class Constants
    {
        public static class ModelNamespaces
        {
            public const string ThatModelWithTheXmlNameSpace = "http://www.w3.org/TR/html4/";
        }

        internal static class ExampleSetting
        {
            public static string Foo { get { return "bar"; } }
        }
    }

    public static class UserSettings
    {
        public static class FileLocations
        {
            public static string DefaultSaveFolder => SettingsHelper.Get(
                SettingsHelper.SettingsType.User, 
                "DefaultSaveFolder", 
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
        }
    }
}

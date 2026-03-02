using NLog;
using NLog.Config;
using NLog.Targets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSSNP_SNSOP.API
{
    public class Utility
    {
        public static void SetupLogging(string location, string fileName, LogLevel level)
        {
            string path = DefaultDataPath();
            //string path = location;
            string archivePath = location + "\\log";

            SetupLogging(fileName, level, path, archivePath);
        }

        public static void SetupLogging(string filename, LogLevel level)
        {
            string path = DefaultDataPath();
            string archivePath = path + "\\log";

            SetupLogging(filename, level, path, archivePath);
        }

        public static void SetupLogging(string filename, LogLevel level, string path, string archivePath)
        {
            LoggingConfiguration config = new LoggingConfiguration();
            FileTarget fileTarget = new FileTarget();

            config.AddTarget("file", fileTarget);
            fileTarget.FileName = path + "\\" + filename;
            //fileTarget.Layout = "${date:format=HH\\:MM\\:ss} ${logger} ${message}";
            fileTarget.ArchiveFileName = archivePath + "\\${shortdate}.{###}." + filename;
            fileTarget.ArchiveEvery = FileArchivePeriod.Day;

            LoggingRule rule = new LoggingRule("*", level, fileTarget);
            config.LoggingRules.Add(rule);

            LogManager.Configuration = config;
        }

        public static string DefaultDataPath()
        {
            string path;

            /* Need to figure out if Isolated Storage can be used
             * to store all log files and sqlite file.
             * 
             * 
            if (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed)
            {
                // Is ClickOnce application, so get isolated storage 
                IsolatedStorageFile store = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null);
                path = store.GetType().GetField("m_RootDir", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(store).ToString();
            }
            else
            {
                // Not ClickOnce application, so use my doc folder
                path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            }
            */
            path = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            //path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            return path;
        }
    }
}

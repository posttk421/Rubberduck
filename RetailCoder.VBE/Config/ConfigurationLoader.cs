﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Runtime.InteropServices;
using System.IO;

namespace Rubberduck.Config
{
    [ComVisible(false)]
    public static class ConfigurationLoader
    {
        private static string configFile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Rubberduck\rubberduck.config";

        public static void SaveConfiguration<T>(T toSerialize)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(toSerialize.GetType());
            using (TextWriter textWriter = new StreamWriter(configFile))
            {
                xmlSerializer.Serialize(textWriter, toSerialize);
            }
        }

        public static Configuration LoadConfiguration()
        {
            try
            {
                using (StreamReader reader = new StreamReader(configFile))
                {
                    var deserializer = new XmlSerializer(typeof(Configuration));
                    return (Configuration)deserializer.Deserialize(reader);
                }
            }
            catch (IOException)
            {
                return GetDefaultConfiguration();
            }
        }

        public static Configuration GetDefaultConfiguration()
        {
            var userSettings = new UserSettings(
                                    new ToDoListSettings(GetDefaultTodoMarkers()),
                                    new CodeInspectionSettings(GetDefaultCodeInspections())
                               );

            return new Configuration(userSettings);
        }

        public static ToDoMarker[] GetDefaultTodoMarkers()
        {
            var note = new ToDoMarker("NOTE:", 0);
            var todo = new ToDoMarker("TODO:", 1);
            var bug = new ToDoMarker("BUG:", 2);

            return new ToDoMarker[] { note, todo, bug };
        }

        public static CodeInspection[] GetDefaultCodeInspections()
        {
            // note: any additional inspection settings added in the future need to be added here
            var optionExplicit = new Config.CodeInspection(InspectionNames.OptionExplicit, Inspections.CodeInspectionType.CodeQualityIssues, Inspections.CodeInspectionSeverity.Warning);
            var implicitVariantReturn = new Config.CodeInspection(InspectionNames.ImplicitVariantReturnType, Inspections.CodeInspectionType.CodeQualityIssues, Inspections.CodeInspectionSeverity.Suggestion);
            var implicitByRef = new Config.CodeInspection(InspectionNames.ImplicitByRef, Inspections.CodeInspectionType.CodeQualityIssues, Inspections.CodeInspectionSeverity.Warning);
            var obsoleteComment = new Config.CodeInspection(InspectionNames.ObsoleteComment, Inspections.CodeInspectionType.MaintainabilityAndReadabilityIssues, Inspections.CodeInspectionSeverity.Suggestion);

            return new CodeInspection[] { optionExplicit, implicitVariantReturn, implicitByRef, obsoleteComment };
        }
    }
}

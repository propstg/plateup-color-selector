using Kitchen;
using System.Collections.Generic;
using System.Linq;

namespace ColorSelector.Prefs {

    public class ColorPreferences {

        public static readonly int APPLIANCE_NORMAL = 0;
        public static readonly int APPLIANCE_HEX = 1;
        public static readonly int MAX_RECENT_COUNT = 20;

        private static Pref ColorApplianceAction = new Pref(ColorSelectorMod.MOD_ID, nameof(ColorApplianceAction));
        private static Pref RecentHexCodes = new Pref(ColorSelectorMod.MOD_ID, nameof(RecentHexCodes));

        public static void registerPreferences() {
            Preferences.AddPreference<int>(new IntPreference(ColorApplianceAction, APPLIANCE_NORMAL));
            Preferences.AddPreference<string>(new StringPreference(RecentHexCodes, ""));
            Preferences.Load();
        }

        public static int getColorApplianceAction => Preferences.Get<int>(ColorApplianceAction);

        public static void setColorApplianceAction(int value) => Preferences.Set<int>(ColorApplianceAction, value);

        public static void addHexCodeToRecentHexCodes(string code) {
            List<string> newCodes = getRecentHexCodes()
                .Prepend(code)
                .Distinct()
                .Where(i => !"".Equals(i))
                .Take(MAX_RECENT_COUNT)
                .ToList();

            Preferences.Set<string>(RecentHexCodes, string.Join(",", newCodes));
        }

        public static List<string> getRecentHexCodes() {
            string value = Preferences.Get<string>(RecentHexCodes);
            return value.Split(',').ToList();
        }
    }
}
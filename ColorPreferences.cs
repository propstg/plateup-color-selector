using Kitchen;

namespace ColorSelector {

    public class ColorPreferences {

        public static readonly int APPLIANCE_NORMAL = 0;
        public static readonly int APPLIANCE_HEX = 1;

        private static Pref ColorApplianceAction = new Pref(ColorSelectorMod.MOD_ID, nameof(ColorApplianceAction));

        public static void registerPreferences() {
            Preferences.AddPreference<int>(new IntPreference(ColorApplianceAction, APPLIANCE_NORMAL));
            Preferences.Load();
        }

        public static int getColorApplianceAction => Preferences.Get<int>(ColorApplianceAction);

        public static void setColorApplianceAction(int value) => Preferences.Set<int>(ColorApplianceAction, value);
    }
}
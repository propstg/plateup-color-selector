using Kitchen;
using KitchenLib;
using KitchenLib.Event;
using System.Reflection;

namespace ColorSelector {

    public class ColorSelectorMod : BaseMod {

        public const string MOD_ID = "blargle.ColorSelector";
        public const string MOD_NAME = "Color Selector";
        public const string MOD_AUTHOR = "blargle";
        public const string MOD_VERSION = "0.0.1";

        public ColorSelectorMod() : base(MOD_ID, MOD_NAME, MOD_AUTHOR, MOD_VERSION, ">=1.1.3", Assembly.GetExecutingAssembly()) { }

        protected override void OnInitialise() {
            ColorPreferences.registerPreferences();
            ModsPreferencesMenu<PauseMenuAction>.RegisterMenu(MOD_NAME, typeof(MainColorMenu<PauseMenuAction>), typeof(PauseMenuAction));
            Events.PreferenceMenu_PauseMenu_CreateSubmenusEvent += (s, args) => {
                args.Menus.Add(typeof(MainColorMenu<PauseMenuAction>), new MainColorMenu<PauseMenuAction>(args.Container, args.Module_list));
                args.Menus.Add(typeof(ColorArrayMenu<PauseMenuAction>), new ColorArrayMenu<PauseMenuAction>(args.Container, args.Module_list));
            };
        }
    }
}
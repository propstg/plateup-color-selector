using ColorSelector.Menu;
using ColorSelector.Prefs;
using Kitchen;
using KitchenLib;
using KitchenLib.Event;
using System.Reflection;

namespace ColorSelector {

    public class ColorSelectorMod : BaseMod {

        public const string MOD_ID = "blargle.ColorSelector";
        public const string MOD_NAME = "Color Selector";
        public const string MOD_AUTHOR = "blargle";
        public const string MOD_VERSION = "0.0.5";

        public ColorSelectorMod() : base(MOD_ID, MOD_NAME, MOD_AUTHOR, MOD_VERSION, ">=1.2.0", Assembly.GetExecutingAssembly()) { }

        protected override void OnInitialise() {
            ColorPreferences.registerPreferences();
            ModsPreferencesMenu<MenuAction>.RegisterMenu(MOD_NAME, typeof(MainColorMenu<MenuAction>), typeof(MenuAction));
            Events.PlayerPauseView_SetupMenusEvent += (s, args) => {
                args.addMenu.Invoke(args.instance, new object[] { typeof(MainColorMenu<MenuAction>), new MainColorMenu<MenuAction>(args.instance.ButtonContainer, args.module_list)});
                args.addMenu.Invoke(args.instance, new object[] { typeof(BuiltInColorPalleteMenu<MenuAction>), new BuiltInColorPalleteMenu<MenuAction>(args.instance.ButtonContainer, args.module_list) });
                args.addMenu.Invoke(args.instance, new object[] { typeof(RecentHexCodesPalleteMenu<MenuAction>), new RecentHexCodesPalleteMenu<MenuAction>(args.instance.ButtonContainer, args.module_list) });
            };
        }
    }
}
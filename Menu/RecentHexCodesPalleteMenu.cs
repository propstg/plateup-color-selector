using Kitchen.Modules;
using UnityEngine;
using System.Collections.Generic;
using ColorSelector.Prefs;

namespace ColorSelector.Menu {

    public class RecentHexCodesPalleteMenu<T> : AbstractColorPalleteMenu<T> {

        public RecentHexCodesPalleteMenu(Transform container, ModuleList module_list) : base(container, module_list) { }

        public override void Setup(int player_id) {
            List<string> list = ColorPreferences.getRecentHexCodes();
            for (int i = 0; i < list.Count; i++) {
                string code = list[i];
                try {
                    Color32 color = ColorUtil.getColorFromHexCode(code);
                    AddColorButton(player_id, color, i, code);
                } catch {

                    Debug.Log($"Encountered error trying to decode hex code: '{code}'");
                }
            }

            AddButton(Localisation["MENU_BACK_SETTINGS"], delegate { RequestPreviousMenu(); });
        }
    }
}
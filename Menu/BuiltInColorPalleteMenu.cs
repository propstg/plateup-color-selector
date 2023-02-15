using Kitchen.Modules;
using UnityEngine;

namespace ColorSelector.Menu {

    public class BuiltInColorPalleteMenu<T> : AbstractColorPalleteMenu<T> {

        public BuiltInColorPalleteMenu(Transform container, ModuleList module_list) : base(container, module_list) { }

        public override void Setup(int player_id) {
            Color color = new Color(0, 0.75f, 0.75f);
            for (int i = 0; i < 20; i++) {
                Color.RGBToHSV(color, out float H, out float S, out float V);
                color = Color.HSVToRGB(H + 0.05f, S, V);
                AddColorButton(player_id, color, i, null);
            }

            AddButton(Localisation["MENU_BACK_SETTINGS"], delegate { RequestPreviousMenu(); });
        }
    }
}
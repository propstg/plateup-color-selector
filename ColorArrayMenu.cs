using HarmonyLib;
using Kitchen;
using Kitchen.Modules;
using KitchenLib;
using UnityEngine;
using TMPro;

namespace ColorSelector {

    public class ColorArrayMenu<T> : KLMenu<T> {

        public ColorArrayMenu(Transform container, ModuleList module_list) : base(container, module_list) { }

        public override void Setup(int player_id) {
            Color color = new Color(0, 0.75f, 0.75f);
            for (int i = 0; i < 20; i++) {
                Color.RGBToHSV(color, out float H, out float S, out float V);
                color = Color.HSVToRGB(H + 0.05f, S, V);
                float x = i < 10 ? 0.5f * (i - 4.5f) : 0.5f * (i - 14.5f);
                float y = i < 10 ? 1f : 1.5f;
                AddColorButton(player_id, color, new Vector2(x, y));
            }

            AddButton(Localisation["MENU_BACK_SETTINGS"], delegate { RequestPreviousMenu(); });
        }

        private ButtonElement AddColorButton(int playerId, Color color, Vector2 position) {
            ButtonElement buttonElement = ModuleDirectory.Add<ButtonElement>(Container, position);
            buttonElement.SetLabel("■");
            buttonElement.OnActivate += delegate {
                ColorUtil.setColorOnPlayersProfile(playerId, color);
            };
            buttonElement.SetSize(0.5f, 0.5f);
            ModuleList.AddModule(buttonElement, position);
            Traverse.Create((object)buttonElement).Field<TextMeshPro>("Label").Value.color = color;
            Traverse.Create((object)buttonElement).Field<TextMeshPro>("Label").Value.fontSize = 50;
            Traverse.Create((object)buttonElement).Field<TextMeshPro>("Label").Value.fontWeight = FontWeight.Bold;
            return buttonElement;
        }
    }
}
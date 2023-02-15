using HarmonyLib;
using Kitchen;
using Kitchen.Modules;
using KitchenLib;
using UnityEngine;
using TMPro;
using Colorblind.Settings;
using ColorSelector.Prefs;

namespace ColorSelector.Menu {

    public abstract class AbstractColorPalleteMenu<T> : KLMenu<T> {

        protected AbstractColorPalleteMenu(Transform container, ModuleList module_list) : base(container, module_list) { }

        protected ButtonElement AddColorButton(int playerId, Color color, int index, string code) {
            Vector2 position = getPositionFromIndex(index);
            ButtonElement buttonElement = ModuleDirectory.Add<ButtonElement>(Container, position);
            buttonElement.SetLabel("■");
            buttonElement.OnActivate += delegate {
                ColorUtil.setColorOnPlayersProfile(playerId, color);
                if (code != null) {
                    ColorPreferences.addHexCodeToRecentHexCodes(code);
                }
            };
            buttonElement.SetSize(0.5f, 0.5f);
            ModuleList.AddModule(buttonElement, position);
            Traverse.Create(buttonElement).Field<TextMeshPro>("Label").Value.color = color;
            Traverse.Create(buttonElement).Field<TextMeshPro>("Label").Value.fontSize = 50;
            Traverse.Create(buttonElement).Field<TextMeshPro>("Label").Value.fontWeight = FontWeight.Bold;
            return buttonElement;
        }

        private Vector2 getPositionFromIndex(int index) => new Vector2(getX(index), getY(index));

        private float getX(int index) => index < 10 ? 0.5f * (index - 4.5f) : 0.5f * (index - 14.5f);

        private float getY(int index) => index < 10 ? 1.5f : 1f;
    }
}
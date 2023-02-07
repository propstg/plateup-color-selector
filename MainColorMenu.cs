using Kitchen;
using Kitchen.Modules;
using KitchenLib;
using System;
using UnityEngine;

namespace ColorSelector {

    public class MainColorMenu<T> : KLMenu<T> {

        public MainColorMenu(Transform container, ModuleList module_list) : base(container, module_list) { }

        public override void Setup(int player_id) {
            addHexCodeButton(player_id);
            AddSubmenuButton("Choose from built in colors", typeof(ColorArrayMenu<T>));

            AddButton(Localisation["MENU_BACK_SETTINGS"], delegate { RequestPreviousMenu(); });
        }

        private void addHexCodeButton(int playerId) {
            AddButton("Enter hex code", delegate {
                RequestSubMenu(typeof(TextEntryMainMenu), true);
                TextInputView.RequestTextInput("Hex code", "", 7, new Action<TextInputView.TextInputState, string>(delegate (TextInputView.TextInputState result, string code) {
                    ColorUtil.setColorFromHexCode(result, code, playerId);
                }));
            });
        }
    }
}
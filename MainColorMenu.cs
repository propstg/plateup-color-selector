using Kitchen;
using Kitchen.Modules;
using KitchenLib;
using System;
using Unity.Entities;
using UnityEngine;

namespace ColorSelector {

    public class MainColorMenu<T> : KLMenu<T> {

        public MainColorMenu(Transform container, ModuleList module_list) : base(container, module_list) { }

        public override void Setup(int player_id) {
            PlayerManager playerManager = World.DefaultGameObjectInjectionWorld.GetExistingSystem<PlayerManager>();
            if (playerManager == null) {
                renderMustBeHostInfo();
                return;
            }
            playerManager.GetPlayer(player_id, out Player player, false);
            CPlayerColour playerColour = playerManager.EntityManager.GetComponentData<CPlayerColour>(player.Entity);

            addHexCodeButton(playerManager, player.Entity, playerColour);
            AddSubmenuButton("Choose from built in colors", typeof(ColorArrayMenu<T>));

            AddButton(Localisation["MENU_BACK_SETTINGS"], delegate { RequestPreviousMenu(); });
        }

        private void renderMustBeHostInfo() {
            AddInfo("This menu is only available when you're the host.");
            AddInfo("1. Set up your profile in single player");
            AddInfo("2. Switch profiles");
            AddInfo("3. Join the multiplayer lobby");
            AddInfo("4. Switch to the profile with the modified color");

            AddButton(Localisation["MENU_BACK_SETTINGS"], delegate { RequestPreviousMenu(); });
        }

        private void addHexCodeButton(PlayerManager playerManager, Entity playerEntity, CPlayerColour playerColour) {
            AddButton("Enter hex code", delegate {
                RequestSubMenu(typeof(TextEntryMainMenu), true);
                TextInputView.RequestTextInput("Hex code", "", 7, new Action<TextInputView.TextInputState, string>(delegate (TextInputView.TextInputState result, string code) {
                    ColorUtil.setColorFromHexCode(result, code, playerManager, playerEntity, playerColour);
                }));
            });
        }
    }
}
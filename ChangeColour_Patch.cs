using HarmonyLib;
using Kitchen;
using System;
using Unity.Entities;
using UnityEngine;

namespace ColorSelector {

    [HarmonyPatch(typeof(ChangeColour), "Perform")]
    public class ChangeColour_Patch {

        public static bool Prefix(ref InteractionData data) {
            if (ColorPreferences.getColorApplianceAction == ColorPreferences.APPLIANCE_NORMAL) {
                return true;
            }

            var player_id = data.Context.Get<CPlayer>(data.Interactor).ID;

            PlayerManager playerManager = World.DefaultGameObjectInjectionWorld.GetExistingSystem<PlayerManager>();
            if (playerManager == null) {
                return true;
            }

            TextInputView.RequestTextInput("Hex code", "", 20, new Action<TextInputView.TextInputState, string>(delegate (TextInputView.TextInputState result, string code) {
                if (result == TextInputView.TextInputState.TextEntryComplete) {
                    playerManager.GetPlayer(player_id, out Player player, false);
                    CPlayerColour playerColour = playerManager.EntityManager.GetComponentData<CPlayerColour>(player.Entity);
                    ColorUtil.setColorFromHexCode(result, code, playerManager, player.Entity, playerColour);
                }
            }));

            return false;
        }
    }
}
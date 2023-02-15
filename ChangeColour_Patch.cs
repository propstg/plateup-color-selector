using ColorSelector.Prefs;
using HarmonyLib;
using Kitchen;
using System;

namespace ColorSelector {

    [HarmonyPatch(typeof(ChangeColour), "Perform")]
    public class ChangeColour_Patch {

        public static bool Prefix(ref InteractionData data) {
            if (ColorPreferences.getColorApplianceAction == ColorPreferences.APPLIANCE_NORMAL) {
                return true;
            }
            
            var playerId = data.Context.Get<CPlayer>(data.Interactor).ID;

            TextInputView.RequestTextInput("Hex code", "", 7, new Action<TextInputView.TextInputState, string>(delegate (TextInputView.TextInputState result, string code) {
                if (result == TextInputView.TextInputState.TextEntryComplete) {
                    ColorUtil.setColorFromHexCode(result, code, playerId);
                }
            }));

            return false;
        }
    }
}
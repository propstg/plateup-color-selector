using ColorSelector.Prefs;
using Kitchen;
using System;
using UnityEngine;

namespace ColorSelector {

    public class ColorUtil {

        public static void setColorFromHexCode(TextInputView.TextInputState result, string code, int playerId) {
            if (code.StartsWith("#")) {
                code = code.Substring(1);
            }

            if (result == TextInputView.TextInputState.TextEntryComplete) {
                setColorOnPlayersProfile(playerId, getColorFromHexCode(code));
                ColorPreferences.addHexCodeToRecentHexCodes(code);
            }
        }

        public static Color32 getColorFromHexCode(string code) {
            int rgb = Convert.ToInt32(code, 16);
            byte red = (byte)((rgb >> 16) & 255);
            byte green = (byte)((rgb >> 8) & 255);
            byte blue = (byte)(rgb & 255);
            return new Color32(red, green, blue, 255);
        }

        public static void setColorOnPlayersProfile(int playerId, Color newColor) {
            PlayerProfile playerProfile = Players.Main.Get(playerId).Profile;
            playerProfile.Colour = newColor;
            Players.Main.RequestProfileUpdate(playerId, playerProfile);
        }
    }
}
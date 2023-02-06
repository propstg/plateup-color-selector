using Kitchen;
using System;
using Unity.Entities;
using UnityEngine;

namespace ColorSelector {

    public class ColorUtil {

        public static void setColorFromHexCode(TextInputView.TextInputState result, string code, PlayerManager playerManager, Entity playerEntity, CPlayerColour playerColour) {
            if (code.StartsWith("#")) {
                code = code.Substring(1);
            }

            if (result == TextInputView.TextInputState.TextEntryComplete) {
                int rgb = Convert.ToInt32(code, 16);
                byte red = (byte)((rgb >> 16) & 255);
                byte green = (byte)((rgb >> 8) & 255);
                byte blue = (byte)(rgb & 255);
                playerColour.Color = new Color32(red, green, blue, 1);
                playerManager.EntityManager.SetComponentData(playerEntity, playerColour);
            }
        }
    }
}
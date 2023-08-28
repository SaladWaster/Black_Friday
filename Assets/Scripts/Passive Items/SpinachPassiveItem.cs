using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinachPassiveItem : PassiveItem
{
    protected override void ApplyModifier()
    {
        // Multiplies current stat by ( 1 + Fraction)
        // 1 is the default
        // e.g Multiplier of 10, 10/100 is 10% or 0.1
        player.currentMight *= 1 + passiveItemData.Multiplier / 100f;
    }
}
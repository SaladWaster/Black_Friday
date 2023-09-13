using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardHatPassiveItem : PassiveItem
{
    protected override void ApplyModifier()
    {
        // Divide health by 100, turning the Multiplier value of passive to be a percentage of Player's health
        // REMEMBER recovery is PER SECOND, dont overtune it
    
        // player.CurrentRecovery = (0 + passiveItemData.Multiplier) / 100f * player.CurrentHealth;
        
        player.CurrentRecovery = passiveItemData.Multiplier * player.MaxHealth / 100f;        /// Recovery is a flat number, initialised at 1 atm

    }
}

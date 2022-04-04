using System;

public class PotionConsumptionDialogue : AffirmationDialogue
{
		public PotionConsumptionDialogue(PotionTypeSO potion, Action callbackAffirmation, Action callbackCancel) : 
				base($"Drink {potion.itemName}?", 
					$"This will restore {potion.healing} of your hitpoints, but you will lose your potion. ", 
					callbackAffirmation, "Drink it!", 
					callbackCancel, "Cancel") { }
}

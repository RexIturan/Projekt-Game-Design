using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Components.Stats
{
		/// <summary>
		/// Represents a stat of an item or an ability such as damage, gold value, etc. 
		/// </summary>
		public class StatBulletPoint : VisualElement
		{
				private static readonly string defaultStyleSheet = "statBulletPoint";
				private static readonly string className = "statBulletPoint";

				private static readonly string damageClassName = "damage";
				private static readonly string healingClassName = "healing";
				private static readonly string rangeClassName = "range";
				private static readonly string costsClassName = "costs";
				private static readonly string defenseClassName = "defense";
				private static readonly string goldvalueClassName = "goldvalue";

				public StatBulletPoint(StatType type, int value)
				{
						styleSheets.Add(Resources.Load<StyleSheet>(defaultStyleSheet));
						AddToClassList(className);

						TextElement stat = new TextElement();

						TextElement valueText = new TextElement();
						valueText.text = value.ToString();

						switch ( type )
						{
								case StatType.DAMAGE:
										stat.text = "Damage: ";
										stat.AddToClassList(damageClassName);
										valueText.AddToClassList(damageClassName);
										break;
								case StatType.HEALING:
										stat.text = "Healing: ";
										stat.AddToClassList(healingClassName);
										valueText.AddToClassList(healingClassName);
										break;
								case StatType.RANGE:
										stat.text = "Range: ";
										stat.AddToClassList(rangeClassName);
										valueText.AddToClassList(rangeClassName);
										break;
								case StatType.COSTS:
										stat.text = "Costs: ";
										stat.AddToClassList(costsClassName);
										valueText.AddToClassList(costsClassName);
										break;
								case StatType.DEFENSE:
										stat.text = "Defense: ";
										stat.AddToClassList(defenseClassName);
										valueText.AddToClassList(defenseClassName);
										break;
								case StatType.VALUE:
										stat.text = "Gold value: ";
										stat.AddToClassList(goldvalueClassName);
										valueText.AddToClassList(goldvalueClassName);
										break;
						}

						Add(stat);
						Add(valueText);
				}
		}

		public enum StatType
		{
				DAMAGE,
				HEALING,
				RANGE,
				COSTS,
				DEFENSE,
				VALUE
		}
}
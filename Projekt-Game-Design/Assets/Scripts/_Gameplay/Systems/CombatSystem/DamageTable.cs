using Ability;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
		/// <summary>
		/// Defines the efficivity of damage types against armor types. 
		/// </summary>
    public static class DamageTable {
				// define order in table 
				private static readonly ArmorType[] ARMOR_TYPES = { ArmorType.Normal, ArmorType.Fortified, ArmorType.Divine };
				private static readonly DamageType[] DAMAGE_TYPES = { DamageType.Normal, DamageType.Piercing, DamageType.Siege, DamageType.Magic, DamageType.Divine };

				// table of damage multiplications, should correspond to order above 
				// note that the first index is the row and the second index the column
				private static readonly float[,] FACTOR_TABLE =
				{
						// normal, piercing, siege, magic, divine
						{ 1.0f,		 1.0f,		 1.0f,	1.0f,	 1.0f}, // normal
						{ 0.0f,		 0.0f,		 1.0f,	0.0f,  0.0f}, // fortified
						{ 0.1f,    0.1f,     0.1f,  0.1f,  1.0f}  // divine
				};

				private static readonly float STANDARD_FACTOR = 1.0f;

				#region For better indexing 

				private static readonly Dictionary<ArmorType, int> tableRow = InitRowIndices();
				private static readonly Dictionary<DamageType, int> tableCol = InitColIndices();

				private static Dictionary<ArmorType, int> InitRowIndices() {
						Dictionary<ArmorType, int> tableRow = new Dictionary<ArmorType, int>();

						for (int i = 0; i < ARMOR_TYPES.Length; i++) {
								tableRow.Add(ARMOR_TYPES[i], i);
						}

						return tableRow;
				}
				
				private static Dictionary<DamageType, int> InitColIndices() {
						Dictionary<DamageType, int> tableCol = new Dictionary<DamageType, int>();

						for (int i = 0; i < DAMAGE_TYPES.Length; i++) {
								tableCol.Add(DAMAGE_TYPES[i], i);
						}

						return tableCol;
				}

				#endregion

				public static float GetFactorForDamageAndArmor(DamageType damageType, ArmorType armorType) {
						if ( tableRow.ContainsKey(armorType) && tableCol.ContainsKey(damageType) )
								return FACTOR_TABLE[tableRow[armorType], tableCol[damageType]];
						else {
								if(!damageType.Equals(DamageType.Healing))
										Debug.LogWarning($"No entry in the damge multiplication table for types {armorType} and {damageType}. ");

								return STANDARD_FACTOR;
						}
				}
    }
}

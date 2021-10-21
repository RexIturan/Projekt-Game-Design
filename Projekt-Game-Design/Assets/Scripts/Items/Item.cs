using UnityEngine;

/// <summary>
/// Attached to a game object of an item saves reference to its scriptable Object
/// </summary>
public class Item : MonoBehaviour
{
    [SerializeField] public ItemSO itemSO;
}

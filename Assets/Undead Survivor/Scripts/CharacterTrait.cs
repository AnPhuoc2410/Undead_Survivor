using UnityEngine;

public class CharacterTrait : MonoBehaviour
{
    public static float Speed => GameManager.instance.playerIndex == 0 ? 1.1f : 1f;
    public static float Damage => GameManager.instance.playerIndex == 0 ? 1.2f : 1f;
    public static float ScannerRange => GameManager.instance.playerIndex == 0 ? 1.2f : 1f;
    public static float WeaponSpeed => GameManager.instance.playerIndex == 1 ? 1.2f : 1f;
    public static float WeaponFireRate => GameManager.instance.playerIndex == 1 ? 0.9f : 1f;

}

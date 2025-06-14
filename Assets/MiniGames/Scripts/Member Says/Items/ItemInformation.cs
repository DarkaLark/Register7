using UnityEngine;

[CreateAssetMenu(menuName = "Memory Game / Item")]
public class ItemInformation : ScriptableObject
{
    public PossibleItems itemID;
    public Color color;
    public AudioClip itemSound;
    public Sprite itemImage;
}
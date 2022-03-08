using UnityEngine;

[CreateAssetMenu(fileName = "New Product", menuName = "Product")]
public class ProductSO : ScriptableObject
{
    public int ProductID;
    public string Price;
    public Sprite ProductIcon;
    public Material HeadMaterial;
    public Material BodyMaterial;
    public Material HandLegsMaterial;
}

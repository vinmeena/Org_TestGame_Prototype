using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Grid Cell Layout Scriptable Object
/// </summary>
[CreateAssetMenu(fileName ="CardCellSettings",menuName ="Card Cell Settings/Create Card Cell Setting")]
public class GridCellSettings : ScriptableObject
{
    public string CardCellName = string.Empty;
    public Vector2Int CardCellGridDimension = Vector2Int.zero;
    public Vector2 CardCellSize = Vector2.zero;
    public Vector2 CardCellSpacing = Vector2.zero;
    public GridLayoutGroup.Constraint CardCellConstraint = GridLayoutGroup.Constraint.Flexible;
    public int CardCellConstraintCount;


    /// <summary>
    /// Validate Function of Invalid Values
    /// </summary>
    private void OnValidate()
    {
        int xSize = CardCellGridDimension.x;
        int ySize = CardCellGridDimension.y;


        if ((xSize * ySize) % 2 != 0)
            Debug.LogError("Not Valid Card Cell Dimesions.\n It should be divisable by 2");

    }

}

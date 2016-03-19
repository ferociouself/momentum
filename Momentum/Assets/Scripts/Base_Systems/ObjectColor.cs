using UnityEngine;
using System.Collections;

public class ObjectColor : MonoBehaviour {
    
    public enum MyColor
    {
        Blue,
        Gray,
        Green,
        Orange,
        Pink,
        Red,
        White,
        Yellow
    }

    public MyColor myColor;

    /// <summary>
    /// Gets the color of the object.
    /// </summary
    public MyColor GetColor() {
        return myColor;
    }

    /// <summary>
    /// Checks if the color matches the color of this ObjectColor
    /// </summary>
    public bool CheckSameColor(MyColor color) {
        return myColor == color;
    }

    /// <summary>
    /// Checks if the two ObjectColor objects have the same color.
    /// </summary>
    public bool CheckSameColor(ObjectColor other) {
        return CheckSameColor(other.GetColor());
    }
}

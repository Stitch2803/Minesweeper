using UnityEngine;

public class Cells
{
    public enum Type
    {
        Empty,
        Mine,
        Number,
    }

    public Vector3Int position;
    public Type type;
    public int number;
    public bool revealed;
    public bool flagged;
    public bool exploded;
    public bool chorded;
}

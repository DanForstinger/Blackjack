using UnityEngine;

[CreateAssetMenu]
public class IntRangeValue : ScriptableObject
{
    public string Title;
    public int Value = 50;
    public int MinValue = 0;
    public int MaxValue = 100;
}

using UnityEngine;

[CreateAssetMenu(fileName = "Prop", menuName = "ScriptableObjects/Props", order = 1)]
public class Prop_Scriptable : ScriptableObject
{
    [System.Serializable]
    public struct PropType
    {
        public Sprite propSprite;
        public float coinValue;
    }
    public PropType[] propType;
}

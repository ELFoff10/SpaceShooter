using UnityEngine;

[CreateAssetMenu(fileName = "Boss", menuName = "Prop/Boss")]
public class TestProperties : ScriptableObject
{
    [SerializeField] private int Health;

    [SerializeField] private int Energy;


}

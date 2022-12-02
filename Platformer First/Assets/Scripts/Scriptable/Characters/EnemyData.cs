using UnityEngine;

[CreateAssetMenu(menuName = "New Enemy", fileName = "Enemy")]
public class EnemyData : ScriptableObject
{
    public new string name;

    public float damage;
    public float speed;
}

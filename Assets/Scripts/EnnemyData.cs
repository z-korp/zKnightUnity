using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/EnemyData", order = 1)]

// Permet de séparer les données de la partie logique de notre jeu
// Permet donc de separer le design data du runtime data
public class EnemyData : ScriptableObject
{
    public int id;
    public string enemyName;
    public string description;
    public Sprite imageEnemy;
    public int health;

    public int movementRange;
    public int attackRange;
    public int attackDamage;

    public AnimationClip idleAnimation;

}

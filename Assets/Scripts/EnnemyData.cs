using UnityEngine;


[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/EnemyData", order = 1)]

// Permet de séparer les données de la partie logique de notre jeu
// Permet donc de separer le design data du runtime data
public class EnemyData : BaseCharacterData
{
    public string description;
    public Sprite imageEnemy;

    // Animations idle pour setup rapide TBD: Create animator such as the player one and remove it from data
    public AnimationClip idleAnimation;

}

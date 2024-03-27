using UnityEngine;

[CreateAssetMenu(fileName = "BaseCharacterData", menuName = "ScriptableObjects/BaseCharacterData", order = 1)]

// Permet de séparer les données de la partie logique de notre jeu
// Permet donc de separer le design data du runtime data
public class BaseCharacterData : ScriptableObject
{
    public int id;
    public string entityName;
    public int health;
    public int movementRange;
    public int attackRange;
    public int attackDamage;

}

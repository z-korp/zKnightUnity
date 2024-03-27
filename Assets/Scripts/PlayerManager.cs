using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    // TBD: Binder value to script that need them. To do so Player Manager could be a singleton so it can be quickly accessed from any script. Only if one instance of player is needed.
    public PlayerData playerData;

    private int id;
    private string playerName;
    public int health;
    public int movementRange;
    public int attackRange;
    public int attackDamage;


    void Start()
    {
        SetupPlayer(playerData);
    }

    void SetupPlayer(PlayerData data)
    {
        id = data.id;
        playerName = data.entityName;
        health = data.health;
        movementRange = data.movementRange;
        attackRange = data.attackRange;
        attackDamage = data.attackDamage;



    }
}

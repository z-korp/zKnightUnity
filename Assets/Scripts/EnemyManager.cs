using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public EnemyData enemyData; // Assignez ceci dans l'inspecteur Unity avec votre ScriptableObject EnemyData

    private Animator animator;
    private AnimatorOverrideController animatorOverrideController;

    private int id;
    private string enemyName;
    public SpriteRenderer spriteRenderer;
    public int health;

    public int movementRange;
    public int attackRange;
    public int attackDamage;

    public AnimationClip idleAnimation;

    void Start()
    {
        SetupEnemy(enemyData);
    }

    void SetupEnemy(EnemyData data)
    {
        id = data.id;
        enemyName = data.enemyName;
        spriteRenderer.sprite = data.imageEnemy; // Assigne l'image de l'ennemi
        health = data.health;

        movementRange = data.movementRange;
        attackRange = data.attackRange;
        attackDamage = data.attackDamage;
        idleAnimation = data.idleAnimation;

        animator = GetComponent<Animator>();
        animatorOverrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        animator.runtimeAnimatorController = animatorOverrideController;
        animatorOverrideController["BaseIdle"] = enemyData.idleAnimation;
    }
}

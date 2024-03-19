using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component not found on the GameObject.");
        }
        else
        {
            Debug.Log("Animator component found, setting WalkDown to true.");
            animator.SetBool("WalkDown", true);
        }
    }

    void Update()
    {
        

        if(Input.GetKeyDown(KeyCode.Space))
        {
            // animator.SetTrigger("Blend Tree Attack");
            Debug.Log("attacknottom");
            animator.Play("Attack");
        }

        if(Input.GetKeyDown(KeyCode.J))
       {
        Debug.Log("J");
          animator.Play("WalkDown");
       }
        // Debug pour v�rifier si l'animation "WalkDown" est jou�e
        if (animator.GetBool("WalkDown"))
        {
          
            //logger la valeur de animator.getAnimatorTransitionInfo
            
        }
        else
        {
           
        }

        // Ici, tu peux ajouter ton input management plus tard...
    }
}
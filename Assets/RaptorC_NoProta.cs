using UnityEngine;

public class RaptorC_NoProta : MonoBehaviour
{
    public Animator animator;
    public int valorAnim = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
       if(valorAnim == 2)
        {
            animator.SetBool("eat",true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision choque)
    {
        if(choque.gameObject.CompareTag("prota"))
        {
          animator.SetTrigger("empujar");
        }

        }
}
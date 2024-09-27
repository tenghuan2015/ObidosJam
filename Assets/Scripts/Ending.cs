using UnityEngine;
using TMPro;

public class Ending : MonoBehaviour
{
    public Animator animator;
    public TMP_Text timerText;
    public TMP_Text Declare;


    void Start()
    {
    
         if (animator == null)
           { animator = GetComponent<Animator>();}

       
    }
    void Fade()
    { 
    animator.SetTrigger("Ending");

        }
    
}

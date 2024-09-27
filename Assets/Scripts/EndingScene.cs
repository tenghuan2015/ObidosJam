using UnityEngine;

public class EndingScene : MonoBehaviour
{
     void Start()
    {
        SoundManager.instance.PlaySound(SoundManager.instance.Footsteps);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

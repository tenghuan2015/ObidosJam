using System.Collections;
using UnityEngine;

public class AttendCall : MonoBehaviour
{
    public GameObject FirstScene, SecondScene;
    public int TimeInBetweenTheScenes = 1;
    public GameObject ImgTransform;
    GameObject tempScene;
    public static bool isCallAnswered = false;
    public static AttendCall instance;
    void Awake()
    {
        instance = this;
        StartFromKnocking();
        //show 1st screen
        //with a delay -> ring the bell
        //unless the phoner is being picked stay there
        //picking phone -> go to next screen...show the silhioutte of person
    }
    public void StartFromKnocking()
    {
        SoundManager.instance.PlaySound(SoundManager.instance.Footsteps);
        ImgTransform.SetActive(true);
        StartScene(FirstScene);
        StartCoroutine(Bell());
        isCallAnswered = false;
    }
    void StartScene(GameObject scene)
    {
        if (tempScene!=null)
        {
            Destroy(tempScene);
        }
        tempScene = Instantiate(scene, ImgTransform.transform);
    }
    IEnumerator Bell()
    {
        print("Footsteps approching sound");
        yield return new WaitForSeconds(1.5f);
        print("Door Knocking Sound keeps on playing till the player attends the call on door");
        StartCoroutine(SoundManager.instance.PlaySoundLoop(SoundManager.instance.DoorKnock, 1.5f));
        StartCoroutine(SoundManager.instance.PlaySupportingSoundLoop(SoundManager.instance.Bell, 0.7f));
        //ring the bell - on loop
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !isCallAnswered) 
        {
            isCallAnswered=true;
            SoundManager.instance.StopSound();
            StartScene(SecondScene);
        }
    }
}

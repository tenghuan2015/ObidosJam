using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttendCall : MonoBehaviour
{
    public GameObject FirstScene, endScene;
    public List<GameObject> Scenes = new List<GameObject>();
    public int TimeInBetweenTheScenes = 1;
    public GameObject ImgTransform;
    GameObject tempScene;
    public static bool isCallAnswered = false, isStart=true;
    public static AttendCall instance;
    public SerialController serialController;
    public int currentSceneNumber = 0;


    void Awake()
    {
        instance = this;
        StartFromKnocking();
    }
    public void StartFromKnocking()
    {
        if (tempScene!=null)
        {
            Destroy(tempScene);
        }
        tempScene = Instantiate(FirstScene, ImgTransform.transform);
        currentSceneNumber++;
        SoundManager.instance.PlaySound(SoundManager.instance.Footsteps);
        ImgTransform.SetActive(true);
        isStart=false;
        StartCoroutine(Bell());
        isCallAnswered = false;


    }
    IEnumerator Bell()
    {
        print("Footsteps approching sound");
        yield return new WaitForSeconds(1.5f);
        print("Door Knocking Sound keeps on playing till the player attends the call on door");

        //serialController.SendSerialMessage("ringA");

        StartCoroutine(SoundManager.instance.PlaySoundLoop(SoundManager.instance.DoorKnock, 1.5f));
       // StartCoroutine(SoundManager.instance.PlaySupportingSoundLoop(SoundManager.instance.Bell, 0.7f));
        //ring the bell - on loop
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !isCallAnswered) 
        {
            isCallAnswered=true;
            SoundManager.instance.StopSound();
            StopAllCoroutines();
            Destroy(tempScene.gameObject);
            print("currentSceneNumber: "+ currentSceneNumber);
            print("Scenes.Count: "+ Scenes.Count);
            if (currentSceneNumber < Scenes.Count) 
            { 
                tempScene = Instantiate(Scenes[currentSceneNumber], ImgTransform.transform);
            }
            else
            {
                print("End Scene end");
            }


        }
    }
}

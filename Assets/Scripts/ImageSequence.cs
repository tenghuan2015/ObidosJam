using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageSequence : MonoBehaviour
{
    public GameObject FirstScene, SecondScene;
    public int TimeInBetweenTheScenes = 1;
    public GameObject DoorScene, ImgScene;
    GameObject tempScene;
    private void Start()
    {
        ImgScene.SetActive(true);
        StartScenes(FirstScene);
        DoorScene.SetActive(false);
    }
    void StartScenes(GameObject scene)
    {
        tempScene = Instantiate(scene, ImgScene.transform);
        //yield return new WaitForSeconds(TimeInBetweenTheScenes);
        //Destroy(tempScene.gameObject);
        //tempScene = Instantiate(SecondScene, ImgScene.transform);
    }
}

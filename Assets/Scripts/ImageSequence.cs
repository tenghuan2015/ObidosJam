using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageSequence : MonoBehaviour
{
    public List<Sprite> InitialScenes = new List<Sprite>();
    public List<GameObject> Scenes = new List<GameObject>();
    public Image placeholderImage;
    public int TimeInBetweenTheScenes = 1;
    public GameObject DoorScene, ImgScene;
    GameObject prevScene;
    private void Start()
    {
        ImgScene.SetActive(true);
        StartCoroutine(StartScenes());
        DoorScene.SetActive(false);
    }
    IEnumerator StartScenes()
    {
        DisplayImage(Scenes[0]);
        yield return new WaitForSeconds(TimeInBetweenTheScenes);
        DisplayImage(Scenes[1]);
        yield return new WaitForSeconds(TimeInBetweenTheScenes);
        DisplayImage(Scenes[2]);
    }
    void DisplayImage(GameObject scene)
    {
        //placeholderImage.sprite = sprite;
        if (prevScene.gameObject != null)
        {
            prevScene.SetActive(false);
        }
        scene.SetActive(true);
         prevScene = scene;
        if (scene == InitialScenes[Scenes.Count - 1]) //if it's the final sprite
        {
            DoorScene.SetActive(true);
            ImgScene.SetActive(false);
        }
    }

    void DisplayImage1(Sprite sprite)
    {
        placeholderImage.sprite = sprite;

        if (sprite == InitialScenes[InitialScenes.Count-1]) //if it's the final sprite
        {
            DoorScene.SetActive(true);
            ImgScene.SetActive(false);
        }
    }
}

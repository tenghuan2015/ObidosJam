using UnityEngine;
using UnityEditor;

public class PhoneInputController : MonoBehaviour
{
    //this is a controller of phone input
    public static PhoneInputController Instance{get;private set;}
    private string lastMsg = "";
    [SerializeField] private SerialController controller;
    [SerializeField] private AudioSource audioSource;
    private void Awake(){
        if(Instance==null){
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else{
            Destroy(gameObject);
        }
    }
    void OnMessageArrived(string msg)
    {
        Debug.Log(msg);
        lastMsg = msg;
    }

    // Invoked when a connect/disconnect event occurs. The parameter 'success'
    // will be 'true' upon connection, and 'false' upon disconnection or
    // failure to connect.
    void OnConnectionEvent(bool success)
    {
        
    }
    // Used in options, to get player's choice
    public int GetPlayerOption(){
        if(lastMsg.Equals("1")){
            return 1;
        }
        else if(lastMsg.Equals("0")){
            return 0;
        }
        else{
            Debug.LogError("lastMsg is not 1 or 0");
            return -1;
        }   
    }
    // Used universally, to get the last message
    public string GetLastMsg(){
        return lastMsg;
    }
    public void PlayAudio(string audioPath){
        // the audio path should be like this: "Assets/Sounds/Dialog/F1.mp3"
        AudioClip clip = Resources.Load<AudioClip>(audioPath);
        if (clip != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
        else
        {
            Debug.LogError("can't find audio file: " + audioPath);
        }
    }
}

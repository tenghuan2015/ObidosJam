using UnityEngine;

public class PhoneInputController : MonoBehaviour
{
    public static PhoneInputController Instance{get;private set;}
    private string lastMsg = "";
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
}

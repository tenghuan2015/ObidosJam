using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using System;
using System.Text;
using UnityEditor;
using UnityEngine.SceneManagement;
public class TypingGameController : MonoBehaviour
{

    public TMP_Text displayText; // 显示数字串的文本
    public TMP_InputField inputField; // 玩家输入框
    public TMP_Text timerText; // 计时器文本
    public bool IsSuccess;
    
    public Animator animator;
    public Animator animator01;
      public Animator animator02;
      public Animator animator03;
    public Animator animator04;
     public Animator animator05;
                       
      public Animator animator06;
       public Animator animator07;
       public Animator animator08;
       public Animator animator09;
       public Animator animator00;


   public Image circularTimerImage; // 公共变量，用于在 Inspector 中赋值

    //private string[] numberStrings = new string[3]; // 存储三组数字串
    private int currentStringIndex = 0; // 当前显示的数字串索引
    private bool gameStarted = false;
    private float remainingTime = 10f; // 游戏时长
    private float totalDuration = 10f;

    private int TryTimes = 4; // 尝试次数

    private string playerInputString = "";

    [SerializeField] private AudioSource audioSource;
    public NumGenerator numGenerator;
    private string currentNum;
    void Start()
    {
        //InitializeNumberStrings();
        UpdateDisplayText();
        StartCoroutine(StartTimer());
        //inputField.onValueChanged.AddListener(OnInputChanged);
        // OnMessageArrived();
        // flashEffect = GetComponent<FlashEffect>();
         if (animator == null)
            animator = GetComponent<Animator>();

    }
    
    public void FailEffect()
    {
         animator.SetTrigger("Flash");
    }
    
    void Update()
    {
        try
        {
            // Debug.Log("Update开始执行");
            // // 您的更新逻辑
            // // ...
            // Debug.Log("准备检查输入");
            // // 检查输入的代码
            // Debug.Log("输入检查完成");

            TimeOut();
        }
        catch (Exception e)
        {
            Debug.LogError($"发生异常: {e.Message}\n{e.StackTrace}");
        }

        
    }

    // 初始化三组预定义的数字串
    /*
void InitializeNumberStrings()
{
    numberStrings[0] = "9202265545";
    numberStrings[1] = "8135377601";
    numberStrings[2] = "1372649268";
}
    */

// 更新显示的数字串
void UpdateDisplayText()
{
         //Debug.Log("Updating display text");
        // 生成一个 0 到 numberStrings.Length - 1 之间的随机数
        //currentStringIndex = UnityEngine.Random.Range(0, numberStrings.Length);
        currentNum = numGenerator.GetCurrentRanNum();
        //displayText.text = "+" + currentNum;
}

    // 启动计时器
    IEnumerator StartTimer()
    {
        UpdateDisplayText();
        Debug.Log("Starting timer");
        if(TryTimes == 4)
        {
            SoundGenerator.Instance.PlayRanSound();
        }
        while (remainingTime >= 0)
        {
            
            circularTimerImage.fillAmount = remainingTime / totalDuration;
            timerText.text = remainingTime.ToString("F0");
            yield return new WaitForSeconds(1f);
            remainingTime -= 1f;
        }
    }
    void TimeOut()
    {
//        Debug.Log(remainingTime);
        if(remainingTime < 0.01f && !IsSuccess)
    { 
        checkFailStatus();
    }
    }

    
    IEnumerator EndGame(bool success)
    {
        gameStarted = false;
        if (success)
        {
            //ResetGame();
            switch (TryTimes)
            {
                case 4:
                    Debug.Log("first success");
                    
                    yield return PlaySoundCoroutine("Assets/Sounds/NoAnswer1.mp3"); // 等待音效播放完成
                    IsSuccess = false;
                    TryTimes -= 1;
                    ResetGame();
                    break;
                case 3:
                    Debug.Log("second success");
                    
                    yield return PlaySoundCoroutine("Assets/Sounds/NoAnswer2.mp3"); // 等待音效播放完成
                    IsSuccess = false;
                    TryTimes -= 1;
                    ResetGame();
                    break;
                case 2:
                    Debug.Log("third success");
                    
                    yield return PlaySoundCoroutine("Assets/Sounds/NoAnswer3.mp3"); // 等待音效播放完成
                    IsSuccess = false;
                    TryTimes -= 1;
                    ResetGame();
                    break;
                case 1:
                    Debug.Log("Congratulations! You won!");
                    yield return PlaySoundCoroutine("Assets/Sounds/Dialog/F1.mp3");
                    TryTimes -= 1;
                    EndingController.Instance.SetEndStatus(true);
                    SceneManager.LoadScene("end");
                    break;
                default:
                    break;
            }
          
            //PhoneInputController.Instance.PlayAudio("Assets/Sounds/Dialog/F1.mp3");

        }
        else
        {
            //ResetGame();
            StopAllCoroutines();
            inputField.text = ""; // 清空输入框
            Debug.Log("Game Over!");
            EndingController.Instance.SetEndStatus(false);
            SceneManager.LoadScene("end");
            //yield return PlaySoundCoroutine("Assets/Sounds/Effect/NoAnswer.mp3");
        }
    }

    // 开始游戏
    public void ResetGame()
    {
        Debug.Log("reset game");
        StopAllCoroutines();
        NumGenerator.Instance.GeneratePhoneNumber();
        gameStarted = true;
        inputField.text = ""; // 清空输入框
        // currentStringIndex = 0;
        remainingTime = 10f;
        UpdateDisplayText();
        playerInputString = "";
        StartCoroutine(StartTimer());
        SoundGenerator.Instance.PlayRanSound();
    }
    
 
    void OnMessageArrived(string msg)
    {
        //Debug.Log(msg);
        switch (msg)
        {
            case "1":
                animator01.SetTrigger("1press");
                break;
            case "2":
                animator02.SetTrigger("2press");
                break;
            case "3":
                animator03.SetTrigger("3press");
                break;
            case "4":
                animator04.SetTrigger("4press");
                break;
            case "5":
                animator05.SetTrigger("5press");
                break;
            case "6":
                animator06.SetTrigger("6press");
                break;
            case "7":
                animator07.SetTrigger("7press");
                break;
            case "8":
                animator08.SetTrigger("8press");
                break;
            case "9":
                animator09.SetTrigger("9press");
                break;
            case "0":
                animator00.SetTrigger("0press");
                break;
        }
        

        if(msg.Length == 1)
        {
            //Debug.Log(msg);
            
            if (playerInputString.Length < currentNum.Length)
            {playSound("Assets/Sounds/Effect/ding.mp3");
        // 获取每个输入的字符并添加到字符串中
            playerInputString += msg;
            inputField.text = playerInputString;
        }
        }
        if(playerInputString.Length == currentNum.Length){
        // 判断输入的字符串是否与目标字符串相等
        bool isEqual = playerInputString.Equals(currentNum);

        if (isEqual)
        {
            Debug.Log("输入正确！");
            // 在这里添加正确输入的处理逻辑
            inputField.text = "Calling...";
            IsSuccess = true;
            StartCoroutine(EndGame(IsSuccess)); // 启动协程
        }
        else
        {
            checkFailStatus();
        }
        }
        
    

        
    
    }
    private void checkFailStatus(){
            // 在这里添加错误输入的处理逻辑
            if (TryTimes > 1)
            {
                TryTimes-=1;
                Debug.Log($"尝试次数剩余：{TryTimes}");
                FailEffect();
                playSound("Assets/Sounds/WrongNumber.mp3");
                ResetGame();
                // 清空输入框并更新显示的数字串
                // inputField.text = "";
                // UpdateDisplayText();
            }
            else
            {
                // 尝试次数用尽，结束游戏
                FailEffect();
                IsSuccess = false;
                remainingTime = 10f;
                UpdateDisplayText();
                StartCoroutine(EndGame(IsSuccess)); // 启动协程
                Debug.Log("尝试次数用尽，游戏结束。");
            
            }

    }
     void OnConnectionEvent(bool success)
    {
        
    }
    private IEnumerator PlaySoundCoroutine(string path)
    {
    // 播放音频
    AudioClip clip = AssetDatabase.LoadAssetAtPath<AudioClip>(path);
    if (clip != null)
    {
        audioSource.clip = clip;
        audioSource.Play();
        // 等待音频播放完毕
        yield return new WaitForSeconds(clip.length);
    }
    else
    {
        Debug.LogError("找不到音频文件: " + path);
    }
    }
    private void playSound(string path){
        StartCoroutine(PlaySoundCoroutine(path));
    }


    }

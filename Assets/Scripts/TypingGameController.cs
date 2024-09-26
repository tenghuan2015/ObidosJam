using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using System;

public class TypingGameController : MonoBehaviour
{

    public TMP_Text displayText; // 显示数字串的文本
    public TMP_InputField inputField; // 玩家输入框
    public TMP_Text timerText; // 计时器文本
    public bool success;

    private string[] numberStrings = new string[3]; // 存储三组数字串
    private int currentStringIndex = 0; // 当前显示的数字串索引
    private bool gameStarted = false;
    private float remainingTime = 10f; // 游戏时长

    private int attemptsLeft = 3; // 剩余尝试次数
    
    // public Text feedbackText;
    void Start()
    {
        InitializeNumberStrings();
        UpdateDisplayText();
        StartCoroutine(StartTimer());
        //inputField.onValueChanged.AddListener(OnInputChanged);
    }

    void Update()
    {
        try
        {
            Debug.Log("Update开始执行");
            // 您的更新逻辑
            // ...
            Debug.Log("准备检查输入");
            // 检查输入的代码
            Debug.Log("输入检查完成");
        }
        catch (Exception e)
        {
            Debug.LogError($"发生异常: {e.Message}\n{e.StackTrace}");
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("检测到回车键被按下");
            // 在这里调用 CompareStrings 方法
            CompareStrings(inputField.text);
        }
        if(remainingTime<=0.01f && !success){
            if (attemptsLeft > 1)
            {
                attemptsLeft-=1;
                Debug.Log($"尝试次数剩余：{attemptsLeft}");
                
                ResetGame();
                // 清空输入框并更新显示的数字串
                // inputField.text = "";
                // UpdateDisplayText();
            }
            else
            {
                // 尝试次数用尽，结束游戏
                EndGame(false);
                Debug.Log("尝试次数用尽，游戏结束。");
            }
        }
    }

    // 初始化三组预定义的数字串
void InitializeNumberStrings()
{
    numberStrings[0] = "1234567890";
    numberStrings[1] = "0987654321";
    numberStrings[2] = "1122334455";
}

// 更新显示的数字串
void UpdateDisplayText()
{   Debug.Log("Updating display text");
    // 生成一个 0 到 numberStrings.Length - 1 之间的随机数
    currentStringIndex = UnityEngine.Random.Range(0, numberStrings.Length);
    displayText.text = "Type: " + numberStrings[currentStringIndex];
}

    // 启动计时器
    IEnumerator StartTimer()
    {Debug.Log("Starting timer");
        while (remainingTime >= 0)
        {
            timerText.text = "Time Left: " + remainingTime.ToString("F1");
            yield return new WaitForSeconds(1f);
            remainingTime -= 1f;
        }
    }

    // // 检查玩家输入
    // public void OnInputChanged(string input)
    // {
    //     Debug.Log("Checking input");
    //     if (!gameStarted) return;

    //     if (input.Trim() == numberStrings[currentStringIndex].Trim())
    //     {
    //         // 成功匹配的代码
    //     }

    //     if (input == numberStrings[currentStringIndex])
    //     {
    //         EndGame(true);
    //     }
    // }

    // 确保方法是公共的，如果您需要从其他脚本访问它
    public void CompareStrings(string inputString)
    {
        string targetString = numberStrings[currentStringIndex]; // 替换为实际的目标字符串

        bool isCorrect = inputString == targetString;
        
        if (isCorrect)
        {
            Debug.Log("输入正确！");
            // 在这里添加正确输入的处理逻辑
            inputField.text = "";
            EndGame(true);
        }
        else
        {
            Debug.Log("输入错误。正确的字符串是：" + targetString);
            // 在这里添加错误输入的处理逻辑
            if (attemptsLeft > 1)
            {
                attemptsLeft-=1;
                Debug.Log($"尝试次数剩余：{attemptsLeft}");

                ResetGame();
                // 清空输入框并更新显示的数字串
                // inputField.text = "";
                // UpdateDisplayText();
            }
            else
            {
                // 尝试次数用尽，结束游戏
                EndGame(false);
                Debug.Log("尝试次数用尽，游戏结束。");
            }
        }
        
        // 可以在这里添加更详细的比较逻辑，例如计算相似度等
    }

    // 结束游戏
    void EndGame(bool success)
    {
        gameStarted = false;
        if (success)
        {
            success = true;
            ResetGame();
            Debug.Log("Congratulations! You won!");

        }
        else
        {
            ResetGame();
            Debug.Log("Game Over!");
        }
    }

    // 开始游戏
    public void ResetGame()
    {
        StopAllCoroutines();
        gameStarted = true;
        inputField.text = ""; // 清空输入框
        // currentStringIndex = 0;
        remainingTime = 10f;
        UpdateDisplayText();
        StartCoroutine(StartTimer());
    }
    
}

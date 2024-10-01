using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
public class BgGenerator : MonoBehaviour
{
    public GameObject phoneNumberPrefab; // 电话号码的预制体
    public int maxPhoneNumbers = 10; // 最大同时显示的电话号码数量
    public Vector2 spawnArea = new Vector2(1920f, 1080f); // 生成区域的大小
    public float spawnInterval = 1f; // 生成新号码的间隔时间
    public float displayDuration = 5f; // 号码显示的持续时间

    private List<GameObject> activePhoneNumbers = new List<GameObject>();
    private float nextSpawnTime;

    void Update()
    {
        // 检查是否到了生成新号码的时间
        if (Time.time >= nextSpawnTime && activePhoneNumbers.Count < maxPhoneNumbers)
        {
            GeneratePhoneNumber();
            nextSpawnTime = Time.time + Random.Range(0, spawnInterval);
        }
    }

    void GeneratePhoneNumber()
    {
        // 随机位置
        Vector3 randomPosition = new Vector3(
            Random.Range(-spawnArea.x / 2, spawnArea.x / 2),
            Random.Range(-spawnArea.y / 2, spawnArea.y / 2),
            0
        );
        Debug.Log(randomPosition);
        // 随机旋转
        Quaternion randomRotation = Quaternion.Euler(0, 0, Random.Range(-30f, 30f));

        // 实例化预制体
        GameObject phoneNumber = Instantiate(phoneNumberPrefab, randomPosition, randomRotation, transform);

        // 设置随机电话号码
        SetRandomPhoneNumber(phoneNumber);

        // 添加到活跃列表
        activePhoneNumbers.Add(phoneNumber);

        // 启动协程以在指定时间后销毁号码
        StartCoroutine(DestroyAfterDelay(phoneNumber, displayDuration));
    }

    void SetRandomPhoneNumber(GameObject phoneNumberObject)
    {
        TextMeshPro textMesh = phoneNumberObject.GetComponentInChildren<TextMeshPro>();
        if (textMesh != null)
        {
            textMesh.text = GenerateRandomPhoneNumber();
        }
    }

    string GenerateRandomPhoneNumber()
    {
        return string.Format("{0:000-0000-0000}", Random.Range(100, 999) * 10000000 + Random.Range(0, 9999999));
    }

    IEnumerator DestroyAfterDelay(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        activePhoneNumbers.Remove(obj);
        Destroy(obj);
    }
}

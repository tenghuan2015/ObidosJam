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

    [System.Serializable]
    public struct SpawnArea
    {
        public Vector2 min;
        public Vector2 max;
    }
    public SpawnArea[] spawnAreas = new SpawnArea[2]; // 定义两个生成区域

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
        // 随机选择一个生成区域
        SpawnArea selectedArea = spawnAreas[Random.Range(0, spawnAreas.Length)];

        // 在选定的区域内随机生成位置
        Vector2 randomPosition = new Vector2(
            Random.Range(selectedArea.min.x, selectedArea.max.x),
            Random.Range(selectedArea.min.y, selectedArea.max.y)
        );
        // 随机旋转
        //Quaternion randomRotation = Quaternion.Euler(0, 0, Random.Range(-30f, 30f));

        // 实例化预制体
        GameObject phoneNumber = Instantiate(phoneNumberPrefab, transform);
        RectTransform rectTransform = phoneNumber.GetComponentInChildren<RectTransform>();
        if (rectTransform != null)
        {
            rectTransform.anchoredPosition = randomPosition;
        }
        
        //GameObject phoneNumber = Instantiate(phoneNumberPrefab, randomPosition, randomRotation, transform);

        // 设置随机电话号码
        SetRandomPhoneNumber(phoneNumber);

        // 添加到活跃列表
        activePhoneNumbers.Add(phoneNumber);

        // 启动协程以在指定时间后销毁号码
        StartCoroutine(DestroyAfterDelay(phoneNumber, displayDuration));
    }

    void SetRandomPhoneNumber(GameObject phoneNumberObject)
    {
        TMP_Text textMesh = phoneNumberObject.GetComponentInChildren<TMP_Text>();
        if (textMesh != null)
        {
            textMesh.text = GenerateRandomPhoneNumber();
        }
    }

    string GenerateRandomPhoneNumber()
    {
        return Random.Range(1000000000, 1000000000 + 1000000000).ToString("D10");
    }

    IEnumerator DestroyAfterDelay(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        activePhoneNumbers.Remove(obj);
        Destroy(obj);
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        foreach (SpawnArea area in spawnAreas)
        {
            Vector3 center = this.transform.TransformPoint(new Vector3((area.min.x + area.max.x) / 2, (area.min.y + area.max.y) / 2, 0));
            Vector3 size = new Vector3(area.max.x - area.min.x, area.max.y - area.min.y, 0);
            Gizmos.DrawWireCube(center, size);
        }
    }
}

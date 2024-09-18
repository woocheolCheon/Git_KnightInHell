using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public GameObject prefab; // 풀에 사용할 프리팹
    public int initialSize = 10; // 초기 풀 크기

    //먼저 들어간게 먼저 나가는 구조 . 선입 선출 
    private Queue<GameObject> objectPool = new Queue<GameObject>();

    private void Start()
    {
        // 초기 객체를 생성하여 풀에 추가
        for (int i = 0; i < initialSize; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false); // 비활성화
            objectPool.Enqueue(obj); //풀에 추가
        }
    }

    public GameObject GetObject()
    {
        if (objectPool.Count > 0)
        {
            // 풀에서 객체를 꺼내어 활성화
            GameObject obj = objectPool.Dequeue();
            obj.SetActive(true);
            return obj;
        }
        else
        {
            // 풀에 객체가 없으면 새로 생성
            GameObject obj = Instantiate(prefab);
            return obj;
        }
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false); // 비활성화하여 풀에 반환
        objectPool.Enqueue(obj);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGManager : MonoBehaviour
{
    private Vector3 oldPos;

    public bool isMove;

    void Start()
    {
        oldPos = transform.localPosition;
    }

    void Update()
    {
        if(isMove == true)
        {
            transform.localPosition -= new Vector3(0.01f, 0, 0);

            if(transform.localPosition.x <= -0.95)
            {
                isMove = false;
                transform.localPosition = oldPos;
            }
            
        }
    }
}

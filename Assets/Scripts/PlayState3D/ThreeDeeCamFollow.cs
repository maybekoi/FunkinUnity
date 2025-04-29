using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreeDeeCamFollow : MonoBehaviour
{
    public Transform targetBF;
    public Transform targetEnemy;
    public bool FollowBF;
    public float smoothSpeed = 0.125f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Transform target = FollowBF ? targetBF : targetEnemy;
        Vector3 desiredPosition = target.position;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
        transform.LookAt(target);
    }
}

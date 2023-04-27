using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatGround : MonoBehaviour
{
    private Vector3 startPos;
    private float repeatLength;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        repeatLength = GetComponent<BoxCollider>().size.z / 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z)
    }
}

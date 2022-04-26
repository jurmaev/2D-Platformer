using System;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length, startpos;
    [SerializeField] private GameObject camera;
    [SerializeField] private float parallaxEffect;

    private void Start()
    {
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void FixedUpdate()
    {
        var temp = (camera.transform.position.x * (1 - parallaxEffect));
        var dist = camera.transform.position.x * parallaxEffect;
        transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);
        if (temp > startpos + length) startpos += length;
        else if (temp < startpos - length) startpos -= length;
    }
}

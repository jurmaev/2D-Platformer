using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float _length, _startpos;

    public GameObject cam;

    [SerializeField] public float parallaxEffect;
    void Start()
    {
        _startpos = transform.position.x;
        _length = GetComponent<SpriteRenderer>().bounds.size.x;  
    }

    void FixedUpdate()
    {
        var temp = cam.transform.position.x * (1 - parallaxEffect);
        var dist = (cam.transform.position.x * parallaxEffect);

        transform.position = new Vector3(_startpos + dist, transform.position.y, transform.position.z);
        if (temp > _startpos + _length) _startpos += _length;
        else if (temp < _startpos - _length) _startpos -= _length;
    }
}

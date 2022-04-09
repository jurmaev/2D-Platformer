using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float _length, _startpos;

    public GameObject cam;

    [SerializeField] public float _parallaxEffect;
    // Start is called before the first frame update
    void Start()
    {
        _startpos = transform.position.x;
        _length = GetComponent<SpriteRenderer>().bounds.size.x;  
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var temp = cam.transform.position.x * (1 - _parallaxEffect);
        var dist = (cam.transform.position.x * _parallaxEffect);

        transform.position = new Vector3(_startpos + dist, transform.position.y, transform.position.z);
        if (temp > _startpos + _length) _startpos += _length;
        else if (temp < _startpos - _length) _startpos -= _length;
    }
}

using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float _length, _startpos;
    public GameObject cam;
    public float parallexEffect;
    private void Start()
    {
        _startpos = transform.position.x;
        _length = GetComponent<SpriteRenderer>().bounds.size.x;
    }
    private void Update()
    {
        var temp = (cam.transform.position.x * (1 - parallexEffect));
        var dist = (cam.transform.position.x * parallexEffect);
        transform.position = new Vector3(_startpos + dist, transform.position.y, transform.position.z);
        if (temp > _startpos + _length) _startpos += _length;
        else if (temp < _startpos - _length) _startpos -= _length;
    }
}

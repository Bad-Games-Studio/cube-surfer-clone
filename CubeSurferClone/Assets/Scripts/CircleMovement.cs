using UnityEngine;

// Stolen from here and adapted.
// https://forum.unity.com/threads/circular-movement.572797/
public class CircleMovement : MonoBehaviour
{
    public float angularSpeed = 1f;
    public float circleRad = 1f;
 
    private Vector3 _center;
    private float _currentAngle;
 
    private void Start ()
    {
        _center = transform.position;
    }
 
    private void Update ()
    {
        _currentAngle -= angularSpeed * Time.deltaTime;
        var offset = new Vector3
        {
            x = Mathf.Sin(_currentAngle),
            y = 0,
            z = Mathf.Cos(_currentAngle)
        };

        var euler = transform.rotation.eulerAngles;
        euler.y = _currentAngle * Mathf.Rad2Deg;
        
        transform.position = _center + circleRad * offset;
        transform.rotation = Quaternion.Euler(euler);
    }
}

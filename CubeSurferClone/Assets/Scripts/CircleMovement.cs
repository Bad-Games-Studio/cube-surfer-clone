using UnityEngine;

// Stolen from here and adapted.
// https://forum.unity.com/threads/circular-movement.572797/
public class CircleMovement : MonoBehaviour
{
    public float speed;
    public float radius;
    public Vector3 center;

    private float _angularSpeed;
    private float _currentAngle;
 
    private void Start ()
    {
        center = transform.position;
        
        RecalculatePositions();
    }

    private void Update()
    {
        if (!Input.GetKey(KeyCode.Mouse0))
        {
            return;
        }
        
        RecalculatePositions();
    }

    private void RecalculatePositions()
    {
        _angularSpeed = speed / radius;

        var deltaAngle = _angularSpeed * Time.deltaTime;
        if (_currentAngle + deltaAngle >= Mathf.PI / 2)
        {
            _currentAngle = Mathf.PI / 2;
        }
        else
        {
            _currentAngle += deltaAngle;
        }
        
        var offset = new Vector3
        {
            x = Mathf.Sin(_currentAngle),
            y = 0,
            z = Mathf.Cos(_currentAngle)
        };

        var euler = transform.rotation.eulerAngles;
        euler.y = _currentAngle * Mathf.Rad2Deg;
        
        transform.position = center + radius * offset;
        transform.rotation = Quaternion.Euler(euler);
    }
}

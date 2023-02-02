using UnityEngine;

public class CarController : MonoBehaviour
{
    private Transform _carTransform;
    public float TurnSpeed;
    public float MoveBorderX;

    private ObjectSpawner _objectSpawner;

    private void Start()
    {
        _carTransform = transform;
        _objectSpawner = FindObjectOfType<ObjectSpawner>();
    }

    private void Update()
    {
        if (_objectSpawner._gameEnded)
        {
            return;
        }

        _carTransform.Translate(Vector3.right * (TurnSpeed * Time.deltaTime * Input.GetAxisRaw("Horizontal")));
        if (_carTransform.position.x > MoveBorderX)
        {
            _carTransform.position = new Vector3(MoveBorderX, _carTransform.position.y, _carTransform.position.z);
        }
        else if (_carTransform.position.x < -MoveBorderX)
        {
            _carTransform.position = new Vector3(-MoveBorderX, _carTransform.position.y, _carTransform.position.z);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Car"))
        {
            _objectSpawner._gameEnded = true;
        }
    }
}
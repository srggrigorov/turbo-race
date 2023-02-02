using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    public float MoveSpeed;
    private Transform _objTransform;

    private ObjectSpawner _objectSpawner;

    private void Start()
    {
        _objTransform = transform;
        _objectSpawner = FindObjectOfType<ObjectSpawner>();
    }

    private void Update()
    {
        if (!_objectSpawner._gameEnded)
        {
            _objTransform.Translate(Vector3.forward * (MoveSpeed * Time.deltaTime));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Destroy"))
        {
            Destroy(gameObject);
        }
    }

    public void SetSpeed(float speed)
    {
        MoveSpeed = speed;
    }
}
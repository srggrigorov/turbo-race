using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CarController :  GasolineInfo
{
    private Transform _carTransform;
    public float TurnSpeed;
    public float MoveBorderX;
    public TextMeshProUGUI scoreInfo;
    public static float saveScore;    
    float score = 0; 
    

    private void Start()
    {
        _carTransform = transform;
        
    }

    private void Update()
    {
       
        if (ObjectSpawner.Instance._gameEnded)
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
        score += 1f;
        scoreInfo.text = "Score: " + score  + " m";
        saveScore = score;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Car"))
        {
            ObjectSpawner.Instance._gameEnded = true;

        }

       
    
    }

    private void OnTriggerEnter(Collider other)
    {
         if (other.gameObject.CompareTag("+fuel"))
        {
            currentGasoline += 1f;
            Destroy(other.gameObject);
        }
    }

}
using UnityEngine;

public class WorldObjectUpdate : MonoBehaviour
{
    public SpriteRenderer sample;
    public int count;
    
    private RandomObject[] _randomObjects;
    
    private void Awake()
    {
        _randomObjects = new RandomObject[count];
        
        for (int i = 0; i < count; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-20, 20), Random.Range(-10, 10), Random.Range(0, 10));
            Quaternion rot = Quaternion.Euler(0, 0, Random.Range(0, 360));
            Vector3 scale = new Vector3(Random.Range(0.5f, 2f), Random.Range(0.5f, 2f), 1);
            RandomObject randomObject = _randomObjects[i];
            sample.color = Random.ColorHSV();
            randomObject.go = Instantiate(sample.gameObject, pos, rot, transform);
            randomObject.go.transform.localScale = scale;
            randomObject.movementSpeed = Random.Range(0.5f, 2f);
            randomObject.rotationSpeed = Random.Range(0f, 100f);
            _randomObjects[i] = randomObject;
        }
    }
    
    private void Update()
    {
        foreach (RandomObject randomObject in _randomObjects)
        {
            randomObject.go.transform.Rotate(0, 0, randomObject.rotationSpeed * Time.deltaTime);
            
            //sin wave
            Vector3 pos = randomObject.go.transform.position;
            pos.x += Mathf.Sin(Time.time * randomObject.movementSpeed) * Time.deltaTime;
            pos.y += Mathf.Cos(Time.time * randomObject.movementSpeed) * Time.deltaTime;
            randomObject.go.transform.position = pos;
        }
    }
    
    private struct RandomObject
    {
        public GameObject go;
        public float rotationSpeed;
        public float movementSpeed;
    }
}

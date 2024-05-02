using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class UIObjectUpdate : MonoBehaviour
{
    public Color startColor;

    public Color endColor;
    
    public float rotationSpeed = 50;
    
    private Image _image;
    
    private Text _text;
    
    private void Awake()
    {
        _image = GetComponent<Image>();
    }
    
    private void Update()
    {
        _image.color = Color.Lerp(startColor, endColor, Mathf.PingPong(Time.time, 1));
        
        transform.Rotate(Vector3.forward, Time.deltaTime * rotationSpeed);
    }
}
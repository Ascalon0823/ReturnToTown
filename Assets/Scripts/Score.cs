using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public Friend friend;

    public TextMeshProUGUI text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        text.text = $"Yeet distance:    {friend.flyingDist:F0}\nDamage to your friend:  {friend.impulse*friend.forceReceived:F0}";
    }
}

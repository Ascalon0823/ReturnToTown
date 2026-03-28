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
        text.text = friend.score.ToString("0");
    }
}

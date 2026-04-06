using TMPro;
using UnityEngine;

public class Scores : MonoBehaviour
{
    public Controls control;
    public TextMeshProUGUI text;
    private float score;

    void Update()
    {
        if (control != null)
        {
            score += (int)(control.speed / 10);
            text.text = "Score: " + score;
        }
    }
}

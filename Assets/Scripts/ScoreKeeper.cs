using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreKeeper : MonoBehaviour {

    public int score = 0;

     Text myText;

    void Start()
    {
        myText = GetComponent<Text>();
        Reset();
    }
	public void Reset()
    {
        score = 0;
        myText.text = score.ToString();
    }

    public void Score(int score)
    {
        this.score += score;
        myText.text = score.ToString();
    }
	
	
}

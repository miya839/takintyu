using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using motokuru;
using UnityEngine.UI;

public class ResultScore : MonoBehaviour {
    public Text ScoreText;
    
    void Start(){
        ScoreText.text = "スコア:" + motokuru.KamadoManager.Score;
    }
}

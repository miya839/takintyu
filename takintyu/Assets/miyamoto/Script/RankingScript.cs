using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using System.IO;
using UnityEngine.UI;
using motokuru;

public class RankingScript : MonoBehaviour {
	private string guitxt = "";
	private string FileName = "RankingText.txt";
	public Text[] Rank;
	private List<int> s = new List<int>();

	void Start(){
		string txt = Application.dataPath; 
		Debug.Log (txt);
		ReadFile ();
		//Rank[0].text = s[0];
		s.Add(motokuru.KamadoManager.Score);
		s.Sort ((a, b) => b - a);
		//FileInfo fi = new FileInfo (Application.dataPath + "/miyamoto/script/" + FileName);
		StreamWriter sw = new StreamWriter(Application.dataPath + "/miyamoto/script/" + FileName, false);
		//sw = fi.AppendText ();
		for (int i = 0; i < 3; i++) {
			Rank [i].text =  "No." + (i+1) + ": " + s [i].ToString();
			sw.WriteLine(s[i]);
			sw.Flush ();
		}
		sw.Close ();
		motokuru.KamadoManager.Score = 0;

	}

	void ReadFile(){
		FileInfo fi = new FileInfo (Application.dataPath + "/miyamoto/script/" + FileName);
		try {
			using (StreamReader sr = new StreamReader (fi.OpenRead (), Encoding.UTF8)) {
				while((guitxt = sr.ReadLine ()) != null){
					s.Add( int.Parse(guitxt));
					Debug.Log(guitxt);
				}
			}
		} catch (Exception e) {
			guitxt = "file not found";
		}
	}
}

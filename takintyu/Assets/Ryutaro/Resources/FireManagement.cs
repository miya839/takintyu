using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using motokuru;

public class FireManagement : MonoBehaviour {

	public float FireQuantity;//とりあえず１０段階と過程 初期値は０
	public int Firewood;//くべてる薪の数
	public int MaxWoodValue = 8;//薪の最大値
	public double FireWoodLoatTime = 2;//薪の消える時間

	private double time;
	private double BurnOutTime;
	private Slider FireGage; 
	private double FireReducedTime = 0.5;//火が小さくなるスピード、初期値は0.1
	private double UpFireQuantity = 0.25;//火の大きくなる量、初期値は0.2


	//public method
	public bool isColectedFire(){
		return this.isJudgedFire ();
	}

	public void ModelateFire(double x){
		this.FireQuantity += (float)x;
	}

	public void FireUp(){
		if(this.FireQuantity < 9f && this.FireQuantity > 1.0f){
			this.ModelateFire (this.UpFireQuantity);
            SoundManager.Instance.playOverapSE(3);
		}
	}

	public void BurningFirewood(){
			this.GetFirewood ();
        	SoundManager.Instance.playOverapSE(2);
	}

	public void Ignition(){
		if(this.FireQuantity <= 1){
			this.FireQuantity = 6.0f;
            SoundManager.Instance.playOverapSE(0);
		}
	}

	public void ReducedFireWood(int x){
		this.Firewood += -x;
	}


	//these method call for Update
	private void reduceFireByTime(){
		if(this.time >= this.FireReducedTime && this.FireQuantity != 0){
			this.ModelateFire (-0.1);
			this.time = 0;
		}
	}

	private void LostFirewood(){
		if(this.Firewood == 0){
			this.FireQuantity = 0f;
		}
	}

	private void ReflectionFireQuantity(){
		this.FireGage.value = this.FireQuantity;
	}


	private void DelayBurnOutSpeedAndUpFire(){
		this.FireReducedTime = this.FireReducedTime + this.Firewood * 0.002f;
		this.UpFireQuantity = this.UpFireQuantity + this.Firewood * 0.001f;
	}

	private void ResetBurnOutSpeedAndUpFire(){
		this.FireReducedTime = 0.5;
		this.UpFireQuantity = 0.25;
	}

	private void UpdateKamado()
	{
		KamadoManager.I.SetMaki ((int)(this.Firewood));
		KamadoManager.I.SetFire((int)(this.FireQuantity / 2.0f));
		KamadoManager.I.SetKamadoJudge (isColectedFire());
	}

	private void FirewoodMinusGuard(){
		if(this.Firewood < 0){
			this.Firewood = 0;
		}
	}
		


	//private method
	private bool isJudgedFire(){
		if(6 <= this.FireQuantity && this.FireQuantity <= 8){
			return true;
		}
		return false;
	}

	private void BurnOutFirewood(){
		if (this.BurnOutTime > this.FireWoodLoatTime && this.FireQuantity >= 1.0f) {
			this.Firewood += -1;
			this.BurnOutTime = 0;
		}
	}

	private void GetFirewood(){
		ChoppingWood cw = GameObject.Find ("Slider").GetComponent<ChoppingWood> ();	
		if (this.Firewood < this.MaxWoodValue && cw.WoodCount > 0) {
			this.Firewood += 1;
			cw.useWood (1);
		} else if(cw.WoodCount > 0){
			cw.useWood (1);
		}
	}



	// Use this for initialization
	void Start () {
		this.FireQuantity = 0;
		this.time = 0;
		this.Firewood = 6;
		this.FireGage = GameObject.Find ("FireGage").GetComponent<Slider> ();
	}
	
	// Update is called once per frame
	void Update () {
		this.time += Time.deltaTime;
		this.BurnOutTime += Time.deltaTime;

		this.reduceFireByTime ();
		this.ReflectionFireQuantity ();
		this.LostFirewood ();
		this.ResetBurnOutSpeedAndUpFire ();
		this.DelayBurnOutSpeedAndUpFire ();
		this.BurnOutFirewood ();
		this.GetFirewood ();
		this.FirewoodMinusGuard ();

		this.UpdateKamado ();
	}
}

using UnityEngine;

[System.Serializable]
public class GameTimer
{
	//@ The limit time.
	public float limitTime;

	//@ The elapsed time.
	public float elapsedTime;

	//@ Gets the time rate(0f ~ 1f).
	public float timeRate
	{
		get;
		private set;
	}

	//@ Gets the left time.
	public float leftTime
	{
		get
		{
			return limitTime - elapsedTime;
		}
	}

	//@ Gets the (1f - timeRate).
	public float timeInverseRate
	{
		get
		{
			return 1f - timeRate;
		}
	}

	//@ is loop.
	public bool isLoop;

	//@ The is time up.
	private bool mIsTimeUp = false;

	/// <summary>
	/// Initializes a new instance of the <see cref="GameTimer"/> class.
	/// </summary>
	/// <param name="_limitTime">_limit time.</param>
	/// <param name="_isLoop">If set to <c>true</c> _is loop.</param>
	/// <param name="_isTimeUp">If set to <c>true</c> _is time up.</param>
	public GameTimer(float _limitTime = 1f, bool _isLoop = false, bool _isTimeUp = false)
	{
		limitTime = _limitTime;
		isLoop = _isLoop;
		if (_isTimeUp)
		{
			SetTimeUp();
		}
	}

	/// <summary>
	/// Update Time.
	/// Return isTimeUp.
	/// </summary>
	public bool Update(float scale = 1.0f)
	{
		if (limitTime == 0.0f)
		{
			timeRate = 1.0f;
			return true;
		}

		elapsedTime += Time.deltaTime * scale;
		timeRate = elapsedTime / limitTime;
		mIsTimeUp = (elapsedTime >= limitTime);
		if (mIsTimeUp)
		{
			timeRate = 1.0f;
			if (isLoop)
			{
				Reset();
			}
			return true;
		}
		else
		{
			return false;
		}
	}

	/// <summary>
	/// leftTime = 0f;
	/// </summary>
	public void Reset()
	{
		elapsedTime = 0.0f;
	}

	/// <summary>
	/// leftTime = 0f;
	/// </summary>
	public void ResetForLoop()
	{
		elapsedTime -= limitTime;
	}

	/// <summary>
	/// elapsedTime = limitTime;
	/// </summary>
	public void SetTimeUp()
	{
		elapsedTime = limitTime;
		timeRate = 1f;
	}

	/// <summary>
	/// Set timeRate, and auto set elapsedTime
	/// </summary>
	/// <param name="_timeRate">_time rate.</param>
	public void SetTimeRate(float _timeRate)
	{
		timeRate = _timeRate;
		elapsedTime = limitTime * timeRate;
	}

	/// <summary>
	/// Determines whether this instance is time up.
	/// </summary>
	/// <returns><c>true</c> if this instance is time up; otherwise, <c>false</c>.</returns>
	public bool IsTimeUp()
	{
		return mIsTimeUp;
	}
}
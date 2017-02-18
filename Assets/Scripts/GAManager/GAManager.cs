using UnityEngine;
using System.Collections;

public class GAManager : MonoBehaviour 
{
	public GoogleAnalyticsV3 googleAnalytics;

	private ScreenLogger _screenLogger;
	public ScreenLogger screenLogger
	{
		get
		{
			return _screenLogger;
		}
	}

	private GameScreenLogger _gameSceenLogger;
	public GameScreenLogger gameSceenLogger
	{
		get
		{
			return _gameSceenLogger;
		}
	}



	void Awake()
	{
		_screenLogger = GetComponent<ScreenLogger>();
		_gameSceenLogger = GetComponent<GameScreenLogger>();
	}

	public void LogEvent(string category, string action, string label, int value = -1)
	{
		var eventHitBuilder = new EventHitBuilder();
		eventHitBuilder.SetEventCategory(category);
		eventHitBuilder.SetEventAction(action);
		eventHitBuilder.SetEventLabel(label);

		if(value != -1)
		{
			eventHitBuilder.SetEventValue(value);
		}

		googleAnalytics.LogEvent(eventHitBuilder);
	}

	public void LogScreen(string title)
	{
		googleAnalytics.LogScreen(title);
	}

	public void LogTime(string category, double value, string name, string label)
	{
		var timingBuilder = new TimingHitBuilder();
		timingBuilder.SetTimingCategory(category);
		timingBuilder.SetTimingInterval((long)value);
		timingBuilder.SetTimingName(name);
		timingBuilder.SetTimingLabel(label);

		googleAnalytics.LogTiming(timingBuilder);
	}



}

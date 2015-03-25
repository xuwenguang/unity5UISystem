using UnityEngine;
using System.Collections;

public static class DebugUtil
{
	public static void Assert(bool condition,string message)
	{
		if(!condition)
		{
			UnityEngine.Debug.Log(message);
		}
	}

}

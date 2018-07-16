using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectManager : MonoBehaviour {
	public static ProjectManager instance;
	public string fileName;
	public string[] experimentOrder;
	public int expNumber;
	public bool fourExpDone = false;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else if (instance != this)
		{
			Destroy(gameObject);
		}

		DontDestroyOnLoad(gameObject);
	}
		
}

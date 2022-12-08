using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cutsceneBGM : MonoBehaviour
{
    public static cutsceneBGM BgMInstance;
    
    private void Awake()
    {
		if(BgMInstance != null && BgMInstance != this)
		{
			Destroy(this.gameObject);
			return;
		}
		
		BgMInstance = this;
		DontDestroyOnLoad(this);
	}
}


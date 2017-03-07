using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 쇼셜 네트워크 매니저
public class CSocialNetworkManager : MonoBehaviour {

    // 계정 타입
    public enum ACCOUNT_TYPE { GEUST, GOOGLE };
    public static ACCOUNT_TYPE accountType = ACCOUNT_TYPE.GEUST;

    // 기본 게임 서버 URL
    public static string baseUrl = "http://localhost/php/flyjumpDB";

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(gameObject);	
	}
	
}

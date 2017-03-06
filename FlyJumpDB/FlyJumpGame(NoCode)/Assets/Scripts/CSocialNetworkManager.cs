using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 소셜 네트워크 매니저
public class CSocialNetworkManager : MonoBehaviour {
    // 계정 타입
    public enum ACCOUNT_TYPE {  GUEST, GOOGLE };
    public static ACCOUNT_TYPE accoutType = ACCOUNT_TYPE.GUEST;

    public static string baseUrl = "http://localhost/php/FlyJumpDB/user_account.php";
    
    void Start () {
        DontDestroyOnLoad(gameObject);
    }
	
	void Update () {
		
	}
}

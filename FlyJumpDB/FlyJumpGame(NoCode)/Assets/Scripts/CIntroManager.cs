using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CIntroManager : MonoBehaviour {

    public Text _msgText;
    
    public void OnGuestLoginButtonClick()
    {
        // 디바이스 고유 정보를 참조함
        string devId = SystemInfo.deviceUniqueIdentifier;
        
        // 가입 및 로그인 처리를 수행함
        JoinOrLogin(devId);
    }

    public void JoinOrLogin(string userId = "")
    {
        _msgText.text = string.Format("[{0}]아이디로 로그인을 시도함.", userId);

        StartCoroutine("JoinOrLoginNetCoroutine", userId);
    }

    IEnumerator JoinOrLoginNetCoroutine(string userId)
    {
        // 가입 및 로그인 url 작성
        string url = CSocialNetworkManager.baseUrl;

        // 유저 아이디값을 POST로 넘겨줌
        WWWForm form = new WWWForm();
        form.AddField("user_id", userId);

        // 가입 및 로그인 요청을 수행함
        WWW www = new WWW(url, form);

        // 통신 지연을 대기함
        yield return www;

        if(www.error == null)
        {
            _msgText.text = "게임 서버와의 통신을 성공함";
            Debug.Log("Data => " + www.text);
        }
        else
        {
            _msgText.text = "게임 서버와의 통신을 실패함";
            Debug.Log("Error => " + www.error);
        }
    }
}

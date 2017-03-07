using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GameData;

public class CIntroManager : MonoBehaviour {

    public Text _msgText;


    public void OnGuestLoginButtonClick()
    {
        // 디바이스 고유 정보를 참조함
        string devId = SystemInfo.deviceUniqueIdentifier;

        // 가입 및 로그인 처리를 수행함
        JoinOrLogin(devId + " 아이디로 로그인을 시도함.", devId);
    }

    // 가입 및 로그인 처리를 수행함
    public void JoinOrLogin(string msg, string userId = "")
    {
        _msgText.text = msg; // 로그인 메시지 출력

        // 가입 및 로그인 수행
        StartCoroutine("JoinOrLoginNetCoroutine", userId);
    }

    IEnumerator JoinOrLoginNetCoroutine(string userId)
    {
        // 가입 및 로그인 url 작성
        string url = CSocialNetworkManager.baseUrl + "/user_account.php";

        // 유저 아이디값을 POST 파라미터로 설정해 줌
        WWWForm form = new WWWForm();
        form.AddField("user_id", userId);

        // 가입 및 로그인 요청을 수행함
        WWW www = new WWW(url, form);

        // 통신을 지연을 대기함
        yield return www;

        if (www.error == null)
        {
            _msgText.text = "게임 서버와의 통신을 성공함";
            Debug.Log("data -> " + www.text);

            // 응답 받은 json 문자열을 Dictionary 객체로 변환함
            Dictionary<string, object> responseData
                = MiniJSON.jsonDecode(www.text) as Dictionary<string, object>;

            // 인증 결과가
            string result_code = responseData["result_code"].ToString().Trim();

            // 성공이면
            if (result_code.Equals("LOGIN_SUCCESS"))
            {
                // 유저 정보를 셋팅함
                GameObject.Find("UserInfo").GetComponent<CUserInfo>().SetUserInfo(responseData);

                _msgText.text = "게임 서버 로그인에 성공함";

                // 비행기 선택 씬으로 이동함
                StartCoroutine("GoSelectSceneCoroutine");
            }
            else
            {
                _msgText.text = "게임 서버 로그인에 실패함";
            }
        }
        else
        {
            _msgText.text = "게임 서버와의 통신을 실패함";
            Debug.Log("error -> " + www.error);
        }

    }

    // 비행기 선택씬으로 이동함
    IEnumerator GoSelectSceneCoroutine()
    {
        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene("Select");
    }
}

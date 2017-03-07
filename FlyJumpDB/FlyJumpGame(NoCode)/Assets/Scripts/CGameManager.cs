using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CGameManager : MonoBehaviour {

    public static bool IsGameStop = false;

    CUserInfo _userInfo;

    public GameObject[] _planePrefabs; // 비행기 프리팹들

    public Text _msgText; // 게임 진행 및 오류 메시지 출력

    private void Awake()
    {
        _userInfo = GameObject.Find("UserInfo").GetComponent<CUserInfo>();
    }

    // Use this for initialization
    void Start () {
        IsGameStop = false;

        // 선택한 비행기 번호를 설정함
        int planeNum = _userInfo._selectPlaneNum;

        // 비행기 번호에 해당하는 비행기를 생성함
        Instantiate(_planePrefabs[planeNum], Vector2.zero, Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // 게임 종료 플래그
    bool isGameEnd = false;

    // 게임 종료
    public void GameEnd(int score)
    {
        if (isGameEnd) return;

        isGameEnd = true;

        // 서버의 유저 별점 정보를 갱신함
        _userInfo.UpdateStarCount(score, gameObject);
    }

    // 게임 종료 완료
    public void GameEndComplte(string msg)
    {
        // 게임 점수 갱신 완료 메시지 출력
        _msgText.text = msg;

        // 종료씬으로 이동함
        UnityEngine.SceneManagement.SceneManager.LoadScene("End");
    }
}

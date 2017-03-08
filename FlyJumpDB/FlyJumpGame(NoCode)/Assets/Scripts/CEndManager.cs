using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CEndManager : MonoBehaviour {
    public Text _logText;
    public Text _starCountText;         // 이번 게임에 획득한 별점
    public Text _totalStarCountText;    // 종합 별점

    CUserInfo _userInfo;

	void Start () {
        _userInfo = FindObjectOfType<CUserInfo>();

        _starCountText.text = _userInfo._bestStarCount.ToString();
        _totalStarCountText.text = _userInfo._totalStarCount.ToString();	
	}
	
	void Update () {
		
	}

    // 게임 재시작 버튼 클릭
    public void OnReStartButtonClick()
    {
        SceneManager.LoadScene("Select");
    }

    public void UpdateStarCountUnityAds(int starCount)
    {
        _userInfo.UpdateStarCount(starCount, gameObject);        
    }

    public void GameEndComplte(string logMessage)
    {
        _totalStarCountText.text = _userInfo._totalStarCount.ToString();

        _logText.text = logMessage;
    }
}

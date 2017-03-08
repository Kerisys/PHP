using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;
public class CUnityAdsManager : MonoBehaviour {
    public string _androidGameId = "";
    public string _iosGameId = "";

    public bool _testMode;
    public Button _adsButton;   // 광고 버튼
    public Text _msgText;

    public CEndManager _endManager;
            
    ShowOptions _adsShowOptions;

    void Start () {
        // 유니티 광고 초기화

        // 현재 플랫폼에 맞는 아이디를 설정함
        string gameId = "";
        #if UNITY_ANDROID
            gameId = _androidGameId;
        #elif UNITY_IOS
            gameId = _iosGameId;
        #endif

        // 유니티 광고 시스템이 제공되는 상태이며 아직 초기화가 되어 있지 않다면
        if(Advertisement.isSupported && !Advertisement.isInitialized)
        {
            // 광고 시스템을 초기화함
            Advertisement.Initialize(gameId);
        }

        _adsShowOptions = new ShowOptions { resultCallback = UnityAdsShowCallback };
    }

    void Update () {
        // 광고 시스템이 초기화가 완료되었고 광고를 볼 준비가 되었다면
        _adsButton.interactable = (Advertisement.isInitialized && Advertisement.IsReady());
	}

    public void OnShowAdsButtonClick()
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show(_adsShowOptions);
        }
    }

    void UnityAdsShowCallback(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                _msgText.text = "광고 시청을 완료함";
                _endManager.UpdateStarCountUnityAds(1000);

                break;
            case ShowResult.Skipped:
                _msgText.text = "광고 시청을 중단함";
                break;
            case ShowResult.Failed:
                _msgText.text = "광고 시청을 실패함";
                break;
        }
    }
}

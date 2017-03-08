using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class CSelectManager : MonoBehaviour {

    CUserInfo _userInfo;

    // 구매 버튼들
    public GameObject[] _purchaseBtns;
    // 게임 시작 버튼들
    public GameObject[] _playBtns;
    // 종합 별점 (코인)
    public Text _totalStarCount;

    public CPurchaseManager _purchaseManager;
    public Text _messageText;

    private void Awake()
    {
        _userInfo = GameObject.Find("UserInfo").GetComponent<CUserInfo>();
    }

    // Use this for initialization
    void Start () {
        SelectSceneInit();
    }

    // 씬 초기화
    void SelectSceneInit()
    {
        // 별점을 코인 포인트로 변환하여 출력함
        _totalStarCount.text = _userInfo._totalStarCount.ToString();

        // 비행기 구매 정보들을 참조하여 구매 가능 여부를 버튼에 설정함
        // 이미 구매한 경우 플레이 버튼이 활성화 됨
        for (int i = 0; i < _userInfo._planeInfos.Length; i++)
        {
            // 현재 번째의 비행체가 구매한 상태면
            if (_userInfo._planeInfos[i]._isPurchase == 1)
            {
                // 구매 버튼 비활성화
                _purchaseBtns[i].SetActive(false);
                // 선택 플레이 버튼 활성화
                _playBtns[i].SetActive(true);
            }
            else
            {
                // 구매 버튼 활성화
                _purchaseBtns[i].SetActive(true);
                // 선택 플레이 버튼 비활성화
                _playBtns[i].SetActive(false);
            }
        }
    }

    // 비행기 선택 및 게임 시작 버튼을 누름
    public void OnPlayButtonClick(int selectPlaneNum)
    {
        // 게임 시작 버튼을 누름 비행기 번호를 설정함
        _userInfo._selectPlaneNum = selectPlaneNum;

        // 게임 씬으로 이동함
        SceneManager.LoadScene("Game");
    }

    public void OnPurchaseButtonClcick(int selectPlaneNum)
    {
        CPurchaseManager.PLANE_TYPE planeType = (CPurchaseManager.PLANE_TYPE)selectPlaneNum;
        
        if(planeType == CPurchaseManager.PLANE_TYPE.YELLOW)
        {
            _purchaseManager.PlanePurchase(planeType, CPurchaseManager.PURCHASE_TYPE.POINT, "", gameObject);
        }
        else
        {
        
        }
    }

    public void PlanePurchaseComplete(string msg)
    {
        _messageText.text = msg;

        SelectSceneInit();
    }
}

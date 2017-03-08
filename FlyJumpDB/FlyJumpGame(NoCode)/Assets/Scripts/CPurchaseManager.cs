using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameData;

public class CPurchaseManager : MonoBehaviour {

    public enum PLANE_TYPE { RED, GREEN, BLUE, YELLOW };
    public enum PURCHASE_TYPE { CASH, POINT };

    CUserInfo _userInfo;

	void Start () {
        _userInfo = FindObjectOfType<CUserInfo>();
            	
	}
	
	void Update () {
		
	}

    public void PlanePurchase(PLANE_TYPE planeType, PURCHASE_TYPE purchaseType, string receiptId, GameObject callback)
    {
        StartCoroutine(PlanePurchaseCoroutine(planeType, purchaseType, receiptId, callback));
    }

    IEnumerator PlanePurchaseCoroutine(PLANE_TYPE planeType, PURCHASE_TYPE purchaseType, string receiptId, GameObject callback)
    {
        string url = CSocialNetworkManager.baseUrl + "/user_plane_purchase.php";

        WWWForm wwwForm = new WWWForm();
        wwwForm.AddField("user_id", _userInfo._userId.Trim());  // 유저 아이디
        wwwForm.AddField("purchase_type", (int)purchaseType);   // 거래 타입 ( 캐쉬, 포인트)
        wwwForm.AddField("plane_type", (int)planeType);         // 비행기 타입
        wwwForm.AddField("receipt_id", receiptId);              // 영수증 번호

        WWW www = new WWW(url, wwwForm);
        yield return www;

        if(www.error == null)
        {
            Dictionary<string, object> responseData = MiniJSON.jsonDecode(www.text) as Dictionary<string, object>;

            string result = responseData["result_code"].ToString().Trim();

            if(result == "PURCHASE_SUCCESS")
            {
                Debug.Log("비행기 구매 성공");
                // 유저 정보에 해당 비행기의 구매 상태를 변경함
                _userInfo._planeInfos[(int)planeType]._isPurchase = 1;

                // 만약 포인트 구매일 경우
                if(purchaseType == PURCHASE_TYPE.POINT)
                {
                    // 유저 정보의 종합 별점(포인트) 점수를 감소함
                    _userInfo._totalStarCount -= 1000;
                }
                callback.SendMessage("PlanePurchaseComplete", "비행기 구매 완료");
            }else
            {
                Debug.Log("비행기 구매 실패");
                callback.SendMessage("PlanePurchaseComplete", "비행기 구매 실패");
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameData;

public class CUserInfo : MonoBehaviour
{

    public string _userId; // 유저 아이디
    public int _bestStarCount; // 최고 별점
    public int _totalStarCount; // 종합 별점
    public int _selectPlaneNum; // 선택한 비행기 번호
    public CPlaneInfo[] _planeInfos; // 비행기 정보 배열

    public void SetUserInfo(Dictionary<string, object> userInfoMap)
    {
        // 계정 정보 딕셔너리를 추출함
        Dictionary<string, object> accountInfoMap = userInfoMap["account_info"]
            as Dictionary<string, object>;

        // 각 정보값들을 추출함
        _userId = accountInfoMap["user_id"].ToString();
        _bestStarCount = int.Parse(accountInfoMap["best_star_count"].ToString());
        _totalStarCount = int.Parse(accountInfoMap["total_star_count"].ToString());


        // 비행기 구매 정보 리스트를 추출함
        List<object> planePurchaseList = userInfoMap["purchase_info"] as List<object>;

        for (int i = 0; i < planePurchaseList.Count; i++)
        {
            //  i번째의 비행기 구매 정보를 추출함
            Dictionary<string, object> planePurchaseInfo =
                planePurchaseList[i] as Dictionary<string, object>;

            // 각 i번째 비행기 구매 정보에 서버로 부터 받아온 구매 정보를 설정함
            _planeInfos[i]._planeId = int.Parse(planePurchaseInfo["purchase_id"].ToString());
            _planeInfos[i]._isPurchase = int.Parse(planePurchaseInfo["is_purchase"].ToString());
        }
    }

    // 별점 업데이트
    public void UpdateStarCount(int starCount, GameObject callback)
    {
        // 서버쪽에 점수갱신이 완료될때까지 임시 점수로 계산을 수행함
        // 성공시 실제 점수에 반영됨

        int tmpBestStartCount = _bestStarCount;
        int tmpTotalStarCount = _totalStarCount;

        // 최고 점수보다 현재 적용 점수가 높다면
        if (tmpBestStartCount < starCount)
        {
            // 임시 최고 점수를 갱신함
            tmpBestStartCount = starCount;
        }

        // 임시 누적 점수를 갱신함
        tmpTotalStarCount += starCount;

        // 서버의 별점 업데이트를 요청함
        StartCoroutine(UpdateStarCountNetCoroutine(tmpBestStartCount,
            tmpTotalStarCount, callback));
    }

    // 서버의 별점을 갱신을 요청함
    IEnumerator UpdateStarCountNetCoroutine(int bestStarCount,
        int totalStarCount, GameObject callback)
    {
        // 별점 정보 갱신 URL 설정
        string url = CSocialNetworkManager.baseUrl + "/user_starcount_update.php";

        // 별점 정보 갱신용 POST 파라미터 설정
        WWWForm wwwForm = new WWWForm();
        wwwForm.AddField("user_id", _userId.Trim());
        wwwForm.AddField("best_star_count", bestStarCount);
        wwwForm.AddField("total_star_count", totalStarCount);

        // 별점 정보 갱신을 요청함
        WWW www = new WWW(url, wwwForm);
        yield return www;

        if (www.error == null)
        {
            Dictionary<string, object> responseData = MiniJSON.jsonDecode(www.text)
                                as Dictionary<string, object>;

            string result = responseData["result_code"].ToString().Trim();
            //string result = "STARCOUNT_UPDATE_SUCCESS";

            Debug.Log("www.text => " + www.text);
            Debug.Log("Update Star Count Result => " + result);

            // 별점 정보 갱신을 성공함
            if (result == "STARCOUNT_UPDATE_SUCCESS") {
                // 서버 업데이트가 성공했다면 게임 데이타를 갱신함
                _bestStarCount = bestStarCount;
                _totalStarCount = totalStarCount;
                Debug.Log("Update Star Count Success");

                callback.SendMessage("GameEndComplte", "점수 갱신 완료");
            }
            else
            {
                callback.SendMessage("GameEndComplte", "점수 갱신 실패");

                Debug.Log("Update Star Count Fail");
            }
        }
        else
        {
            Debug.Log(www.error);
        }
    }
}
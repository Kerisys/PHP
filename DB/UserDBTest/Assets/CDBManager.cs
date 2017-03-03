using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameData; // MiniJSON

public class CDBManager : MonoBehaviour {
    public InputField _findInputField;
    public InputField _idInputField;
    public InputField _nameInputField;
    public InputField _pwInputField;
    public InputField _scoreInputField;
    public InputField _bestScoreInputField;

    public Text _msgText;

    public void OnFindButtonClick()
    {
        StartCoroutine("FindRequestCoroutine");
    }

    IEnumerator FindRequestCoroutine()
    {
        string url = "http://localhost/php/DB/find.php?id="+ _findInputField.text;

        WWW www = new WWW(url);
        yield return www;

        if (www.error == null)
        {
            Debug.Log("data => " + www.text);

            Dictionary<string, object> data = (Dictionary<string, object>)MiniJSON.jsonDecode(www.text);

            string result_code = data["result_code"].ToString();

            if (result_code.Equals("FIND_SUCCESS"))
            {
                Debug.Log("데이타 조회 성공");
                _msgText.text = "데이타 조회 성공";

                Dictionary<string, object> resultData = (Dictionary<string, object>) data["result_data"];

                _idInputField.text = resultData["id"].ToString();
                _nameInputField.text = resultData["name"].ToString();
                _pwInputField.text = resultData["password"].ToString();
                _scoreInputField.text = resultData["score"].ToString();
                _bestScoreInputField.text = resultData["best_score"].ToString();
            }
            else
            {
                Debug.Log("데이타 조회 실패");
                _msgText.text = "데이타 조회 실패";
            }
            _msgText.text = result_code;

        }
        else
        {
            Debug.Log("서버 요청 및 응답 실패");

            _msgText.text = "서버 요청 및 응답 실패";
        }
    }

    public void OnAddButtonClick()
    {
        StartCoroutine("AddRequestCoroutine");
    }

    IEnumerator AddRequestCoroutine()
    {
        string url = "http://localhost/php/DB/add.php";

        WWWForm form = new WWWForm();
        form.AddField("id", _idInputField.text);
        form.AddField("name", _nameInputField.text);
        form.AddField("pw", _pwInputField.text);
        form.AddField("score", _scoreInputField.text);
        form.AddField("best_score", _bestScoreInputField.text);
        
        WWW www = new WWW(url, form);

        yield return www;

        if(www.error == null)
        {
            Debug.Log("data => " + www.text);
             
            Dictionary<string,object> data = (Dictionary<string,object>) MiniJSON.jsonDecode(www.text);

            string result_code = data["result_code"].ToString();
            if (result_code.Equals("ADD_SUCCESS"))
            {
                Debug.Log("데이타 추가 성공");
                _msgText.text = "데이타 추가 성공";
            }
            else
            {
                Debug.Log("데이타 추가 실패");
                _msgText.text = "데이타 추가 실패";
            }
            _msgText.text = result_code;
        }
        else
        {
            Debug.Log("서버 요청 및 응답 실패");

            _msgText.text = "서버 요청 및 응답 실패";
        }       
    }

    public void OnModifyButtonClick()
    {
        StartCoroutine("ModifyRequestCoroutine");
    }

    IEnumerator ModifyRequestCoroutine()
    {
        string url = "http://localhost/php/DB/modify.php";

        WWWForm form = new WWWForm();
        form.AddField("id", _idInputField.text);
        form.AddField("name", _nameInputField.text);
        form.AddField("pw", _pwInputField.text);
        form.AddField("score", _scoreInputField.text);
        form.AddField("best_score", _bestScoreInputField.text);

        WWW www = new WWW(url, form);

        yield return www;

        if (www.error == null)
        {
            Debug.Log("data => " + www.text);

            Dictionary<string, object> data = (Dictionary<string, object>)MiniJSON.jsonDecode(www.text);

            string result_code = data["result_code"].ToString();
            if (result_code.Equals("MODIFY_SUCCESS"))
            {
                Debug.Log("데이타 수정 성공");
                _msgText.text = "데이타 수정 성공";
            }
            else
            {
                Debug.Log("데이타 수정 실패");
                _msgText.text = "데이타 수정 실패";
            }
            _msgText.text = result_code;     
        }
        else
        {
            Debug.Log("서버 요청 및 응답 실패");

            _msgText.text = "서버 요청 및 응답 실패";
        }
    }

    public void OnDeleteButtonClick()
    {
        StartCoroutine("DeleteRequestCoroutine");
    }

    IEnumerator DeleteRequestCoroutine()
    {
        string url = "http://localhost/php/DB/delete.php";

        WWWForm form = new WWWForm();
        form.AddField("id", _idInputField.text);
        form.AddField("name", _nameInputField.text);
        form.AddField("pw", _pwInputField.text);
        form.AddField("score", _scoreInputField.text);
        form.AddField("best_score", _bestScoreInputField.text);

        WWW www = new WWW(url, form);

        yield return www;

        if (www.error == null)
        {
            Debug.Log("data => " + www.text);

            Dictionary<string, object> data = (Dictionary<string, object>)MiniJSON.jsonDecode(www.text);

            string result_code = data["result_code"].ToString();
            if (result_code.Equals("DELETE_SUCCESS"))
            {
                Debug.Log("데이타 삭제 성공");
                _msgText.text = "데이타 삭제 성공";
            }
            else
            {
                Debug.Log("데이타 삭제 실패");
                _msgText.text = "데이타 삭제 실패";
            }
            _msgText.text = result_code;

            _idInputField.text = "";
            _nameInputField.text = "";
            _pwInputField.text = "";
            _scoreInputField.text = "";
            _bestScoreInputField.text = "";
        }
        else
        {
            Debug.Log("서버 요청 및 응답 실패");

            _msgText.text = "서버 요청 및 응답 실패";
        }
    }

}

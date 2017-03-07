using UnityEngine;
using System.Collections;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CPlane : MonoBehaviour {

    Rigidbody2D _rigidbody2d;

    public float _riseForce;

    public Text _starCountText;

    public CGameManager _gameManager; // 게임 매니저

    void Awake()
    {
        _rigidbody2d = GetComponent<Rigidbody2D>();
    }

	// Use this for initialization
	void Start () {
        _starCountText = GameObject.Find("CountText").GetComponent<Text>();
        _gameManager = GameObject.Find("GameManager").GetComponent<CGameManager>();
    }
	
	// Update is called once per frame
	void Update () {
	
        // 비행체가 화면을 넘어갔다면
        if (transform.position.y > 5.5f || transform.position.y < -5.5f)
        {
            // 게임 정지
            CGameManager.IsGameStop = true;
        }

        if (transform.position.y < -5.5f)
        {
            GameEnd();
        }

        if (Input.anyKeyDown && !CGameManager.IsGameStop)
        {
            // 비행체의 속도를 0으로 초기화해 줌
            _rigidbody2d.velocity = Vector2.zero;
            _rigidbody2d.AddForce(new Vector2(0, _riseForce));
        }

	}

    // 충돌 시작 이벤트 (IsTrigger == false 일때 발생함)
    void OnCollisionEnter2D(Collision2D collision)
    // void OnCollisionStay2D(..) : 충돌 중 (IsTrigger == false 일때 발생함)
    // void OnCollisionExit2D(..) : 충돌 끝 (IsTrigger == false 일때 발생함)
    {
        if (collision.gameObject.tag == "Column")
        {
            CGameManager.IsGameStop = true;
            Invoke("GameEnd", 3f);
        }
    }

    // 게임 종료 처리
    void GameEnd()
    {      
        // 현재 별점을 게임 매니저쪽으로 전달함
        _gameManager.GameEnd(int.Parse(_starCountText.text));
    }


    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Item")
        {
            Destroy(collider.gameObject);

            int score = int.Parse(_starCountText.text);
            score++;
            _starCountText.text = score.ToString();
        }
    }
} 

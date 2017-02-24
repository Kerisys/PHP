<?php
	//클래스 선언
	class GameUser{
		// 멤버 변수 선언
		var $id = "user";
		var $pw = "0000";
		// 유저 정보 설정
		function RequestUserInfo(){
			$id = $_GET["user_id"];
			$pw = $_GET["user_pw"];

			$this->id = $id;
			$this->pw = $pw;
		}
		// 유저 정보 보기
		function ResponseUserInfo(){
			// 결과 딕셔너리 생성
			$response_data = array(
				"id" => $this->id,
				"pw" => $this->pw
			);
			// 딕셔너리를 json으로 인코딩해서 보냄
			echo json_encode($response_data);
		}
	}

	// PHP 객체 생성
	$user = new GameUser();
	// 유저 정보 설정
	$user->RequestUserInfo();
	// 유저 정보 보기
	$user->ResponseUserInfo();

?>
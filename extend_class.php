<?php

	// 클래스 선언
	class User {
		// 멤버 변수 선언
		var $id = "user";
		var $pw = "0000";
		// 유저 정보 설정
		function RequestUserInfo() {
			$id = $_GET["user_id"];
			$pw = $_GET["user_pw"];

			$this->id = $id;
			$this->pw = $pw;
		}
		// 유저 정보 보기
		function ResponseUserInfo() {
			// 결과 딕셔너리 생성
			$response_data = array(
				"id" => $this->id,
				"pw" => $this->pw
			);
			// 딕셔너리를 json으로 인코딩해서 보냄
			echo json_encode($response_data);
		}
	}

	// User 클래스를 상속함 GameUser 클래스를 선언함
	class GameUser extends User {
		// 자식 클래스 멤버 추가
		var $playTime = "000";

	    // 자식 클래스 메소드 추가
		function RequestGameUserInfo() {
			// parent 키워드를 통해 부모 메소드를 호출
			parent::RequestUserInfo();

			// 자식 멤버 변수값 설정
			$this->playTime = $_GET["play_time"];
		}
	    
	    // 부모 메소드를 오버라이드 함
	    function ResponseUserInfo() {
	    	// 결과 딕셔너리 생성
	    	$response_data = array(
	    		"id" => $this->id,
	    		"pw" => $this->pw,
	    		"playTime" => $this->playTime
	    		);
	    	// 배열을 json으로 인코딩해서 응답함
	    	echo json_encode($response_data);
	    }
	}

	// PHP 객체 생성
	$user = new GameUser();
	// 유저 정보 설정
	$user->RequestGameUserInfo();
	// 유저 정보 보기
	$user->ResponseUserInfo();
?>
<?php
	$id = $_POST["id"];
	$name = $_POST["name"];
	$pw = $_POST["pw"];
	$score = $_POST["score"];
	$best_score = $_POST["best_score"];

	// insert 작업 순서
	// 1. 데이터베이스 서버 연결
	// 2. 삽입 쿼리 작성
	// 3. 쿼리 실행
	$connect = mysqli_connect(
		"localhost",
		"root",
		"autoset",
		"game_db"
		) or die("Error".mysqli_error($connetxt));

	// 삽입 쿼리 작성
	$sql = "INSERT INTO user_tb(id, name, password,score, best_score) VALUES ('".$id."', '".$name."', '".$pw."', ".$score.", ".$best_score.")";

	//echo $sql;

	// 작성한 SQL를 실행함
	$data = mysqli_query($connect, $sql);

	// 쿼리 실행의 성공 카운트를 구함
	$success_query_count = mysqli_affected_rows($connect);

	// 응답 데이타
	$result_data = array("result_code" => "ADD_FAIL");
	// 쿼리 실행이 1개 이상 성공했다면
	if($success_query_count > 0)
	{	// 성공 결과값 저장
		$result_data["result_code"] = "ADD_SUCCESS";
	}

	// 응답 데이타 전송
	echo json_encode($result_data);

	// 데이터 베이스 서버와 연결을 해제함
	mysqli_close($connect);
?>
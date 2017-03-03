<?php
	$id = $_POST["id"];
	$name = $_POST["name"];
	$pw = $_POST["pw"];
	$score = $_POST["score"];
	$best_score = $_POST["best_score"];

	// UPDATE 작업 순서
	// 1. 데이터베이스 서버 연결
	// 2. 수정 쿼리 작성
	// 3. 쿼리 실행
	// 4. 응답 데이타 전손
	// 5. 데이터베이스 연결 해제
	$connect = mysqli_connect(
		"localhost",
		"root",
		"autoset",
		"game_db"
		) or die("Error".mysqli_error($connetxt));

	// 수정 쿼리 작성
	$sql = "UPDATE user_tb SET name='".$name."', password='".$pw."', score=".$score.", best_score=".$best_score." WHERE id='".$id."'";

	// echo $sql;	// Debug용 쿼리 출력
	// exit();		// php문을 빠져나옴.

	// 작성한 SQL를 실행함
	$data = mysqli_query($connect, $sql);

	// 쿼리 실행의 성공 카운트를 구함
	$success_query_count = mysqli_affected_rows($connect);

	// 응답 데이타
	$result_data = array("result_code" => "MODIFY_FAIL");
	// 쿼리 실행이 1개 이상 성공했다면
	if($success_query_count > 0)
	{	// 성공 결과값 저장
		$result_data["result_code"] = "MODIFY_SUCCESS";
	}

	// 응답 데이타 전송
	echo json_encode($result_data);

	// 데이터 베이스 서버와 연결을 해제함
	mysqli_close($connect);
?>
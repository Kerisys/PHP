<?php
	$id = $_POST["id"];

	// delete 작업 순서
	// 1. 데이터베이스 서버 연결
	// 2. 삭제 쿼리 작성
	// 3. 쿼리 실행
	$connect = mysqli_connect(
		"localhost",
		"root",
		"autoset",
		"game_db"
		) or die("Error".mysqli_error($connetxt));

	// 삭제 쿼리 작성
	$sql = "DELETE FROM user_tb WHERE id = '".$id."'";

	// echo $sql;
	// exit();

	// 작성한 SQL를 실행함
	$data = mysqli_query($connect, $sql);

	// 쿼리 실행의 성공 카운트를 구함
	$success_query_count = mysqli_affected_rows($connect);

	// 응답 데이타
	$result_data = array("result_code" => "DELETE_FAIL");
	// 쿼리 실행이 1개 이상 성공했다면
	if($success_query_count > 0)
	{	// 성공 결과값 저장
		$result_data["result_code"] = "DELETE_SUCCESS";
	}

	// 응답 데이타 전송
	echo json_encode($result_data);

	// 데이터 베이스 서버와 연결을 해제함
	mysqli_close($connect);
?>
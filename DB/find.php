<?php
	$id = $_GET["id"];

	// 검색 작업 순서
	// 1. 데이터베이스 서버 연결
	// 2. 조회 쿼리 작성
	// 3. 쿼리 실행
	// 4. 조회 결과 추출
	// 5. 조최 결과 전송
	// 6. 데이타베이스 연결 해제

	// 데이타베이스 서버 연결
	$connect = mysqli_connect(
		"localhost",	// 데이타베이스 서버 주소
		"root",			// 관리자 아이디
		"autoset",		// 비밀번호
		"game_db"		// 데이타베이스 이름
		) or die("Error".mysqli_error($connetxt));

	// 검색 쿼리 작성
	$sql = "SELECT id, name, password, score, best_score FROM user_tb WHERE id = '".$id."'";

	//echo $sql;

	// 작성한 SQL를 실행함
	$data = mysqli_query($connect, $sql);

	// 응답 데이타
	$result_data = array("result_code" => "FIND_FAIL");
	// 조회 데이타가 1개 이상이면
	if($data->num_rows > 0)
	{	// 성공 결과값 저장
		$result_data["result_code"] = "FIND_SUCCESS";
		// 조회한 데이타의 마지막 데이타 행을 추출함
		$result_data["result_data"] = mysqli_fetch_assoc($data);
	}

	// 응답 데이타 전송
	echo json_encode($result_data);

	// 데이터 베이스 서버와 연결을 해제함
	mysqli_close($connect);
?>
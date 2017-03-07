<?php
	// 업데이트할 별 정보를 전송 받음
	$user_id = $_POST["user_id"];
	$best_star_count = $_POST["best_star_count"];
	$total_star_count = $_POST["total_star_count"];

	// 데이타 베이스 연결
	$connect = mysqli_connect(
		"localhost", "root", "autoset",
		"flyjump_db"
	) or die("Error".mysqli_error($connect));

	// 업데이트 결과 정보 객체를 생성함

	$responseData = array(
		"result_code" => "STARCOUNT_UPDATE_FAIL"
	);

	// 별 갯수 업데이트 쿼리를 작성함
	$sql = "UPDATE user_tb SET best_star_count=".$best_star_count.", total_star_count=".$total_star_count." WHERE user_id ='".$user_id."'";

	//echo $sql;

	$result = mysqli_query($connect, $sql);

	$row_count = mysqli_affected_rows($connect);

	if($row_count > 0){
		$responseData["result_code"] = "STARCOUNT_UPDATE_SUCCESS";
	}

	echo json_encode($responseData);

	mysqli_close($connect);
?>
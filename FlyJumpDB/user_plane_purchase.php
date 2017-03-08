<?php
	// 유저 아이디
	$user_id = $_POST["user_id"];
	// 구매 타입(캐쉬:0, 포인트:1)
	$purchase_type = $_POST["purchase_type"];
	// 비행기 타입
	$plane_type_num = $_POST["plane_type"];
	// 영수증 아이디
	$receipt_id = $_POST["receipt_id"];

	$plane_type_array = array('RED', 'GREEN', 'BLUE', 'YELLOW');

	// 데이타 베이스 연결
	$connect = mysqli_connect(
		"localhost", "root", "autoset",
		"flyjump_db"
	) or die("Error".mysqli_error($connect));

	// 업데이트 결과 정보 객체를 생성함
	$responseData = array(
		"result_code" => "PURCHASE_FAIL"
	);

	mysqli_query($connect, "START TRANSACTION");

	$result1 = $result2 = 1;

	if($purchase_type == 1){	// 포인트 결제
		$sql = "UPDATE user_tb SET total_star_count = total_star_count-1000 WHERE user_id = '".$user_id."'";

		mysqli_query($connect, $sql);

		$result1 = mysqli_affected_rows($connect);
	}


	// 구매한 비행기의 구매정보를 업데이트 함
	$sql = "UPDATE plane_purchase_tb SET is_purchase=TRUE, purchase_date=NOW(), receipt_id= '".$receipt_id."' WHERE user_id = '".$user_id."' AND plane_type = '".$plane_type_array[$plane_type_num]."'";

	mysqli_query($connect, $sql);

	$result2 = mysqli_affected_rows($connect);

	if($result1 > 0 and $result2 > 0){
		mysqli_query($connect, "COMMIT");	// 트렌젝션 커밋

		$responseData["result_code"] = "PURCHASE_SUCCESS";
	}else {
		mysqli_query($connect, "ROLLBACK");
	}

	echo json_encode($responseData);

	mysqli_close($connect);
?>
<?php
	// GET방식으로 데이타를 요청 받음(Request)
	$v1 = $_GET["value1"];	// value1 파라미터 추출
	$v2 = $_GET["value2"];	// value2 파라미터 추출	
	$oper = $_GET["oper"];	// 0:덧셈, 1:뺄셈

	// 응답 데이타 객체(딕셔너리) 생성
	$response_data = array(
		'value1' => $v1,
		'value2' => $v2
	);

	switch ($oper) {
		case 0:
			$response_data["result"] = ($v1+$v2);
			break;
		case 1:
			$response_data["result"] = ($v1-$v2);
			break;		
		default:
			# code...
			break;
	}

	// 응답 데이타 객체(딕셔너리)를 json 데이타 포맷으로 변경함
	echo json_encode($response_data);

?>
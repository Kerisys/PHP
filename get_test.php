<?php
	// GET방식으로 데이타를 요청 받음(Request)
	$v1 = $_GET["value1"];	// value1 파라미터 추출
	$v2 = $_GET["value2"];	// value2 파라미터 추출	

	// 응답 데이타 객체(딕셔너리) 생성
	$response_data = array(
		'value1' => $v1,
		'value2' => $v2,
		'sum' => ($v1+$v2),
		'sub' => ($v1-$v2)
	);

	// 응답 데이타 객체(딕셔너리)를 json 데이타 포맷으로 변경함
	echo json_encode($response_data);

?>
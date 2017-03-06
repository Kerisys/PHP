<?php
  
    // 유저 정보를 검색함
    function find_user($connect, $user_id, &$responseData)
    {
        // 유저 정보 조회 쿼리 작성
        $sql = "SELECT * FROM user_tb WHERE user_id='".$user_id."'";
  
        // 유저 정보 조회 쿼리 실행
        $result_data = mysqli_query($connect, $sql);
  
        if ($result_data == null) return FALSE;
  
        if ($result_data->num_rows > 0) {
  
            // 해당 유저의 레코드 정보를 불러옴
            $responseData["account_info"] =
                mysqli_fetch_assoc($result_data);
  
            // 유저의 구매 정보 쿼리 작성
            $sql = "SELECT * FROM plane_purchase_tb WHERE user_id='".$user_id."'";
  
            // 유저의 구매 정보 쿼리 실행
            $result_data = mysqli_query($connect, $sql);
  
            if ($result_data == null) return FALSE;
  
            if ($result_data->num_rows > 0) {
  
                // 레코드(행) 단위로 정보를 불러옴
                $i = 0;
                while ($row = mysqli_fetch_assoc($result_data)) {
                    $responseData["purchase_info"][$i++] = $row;
                }
  
                $responseData["result_code"] = "LOGIN_SUCCESS";
                return TRUE;
            }
  
            return FALSE;
        }
    }
  
    $user_id = $_POST["user_id"];	
  
	$responseData = array(
        "result_code" => "LOGIN_FAIL",
        "account_info" => array(),
        "purchase_info" => array()
    );

    if ($user_id == "" || $user_id == null)
  	{
  		echo json_encode($responseData);
  		exit();
  	}

    $connect = mysqli_connect(
        "localhost",
        "root", "autoset", "flyjump_db"
    ) or die("Error".mysqli_error($connect));  
  
  
    // 유저 정보가 존재하는지를 조회함
    $result = find_user($connect, $user_id, $responseData);
  
    // 해당 아이디를 가진 유저가 존재하지 않으면
    if ($result == FALSE) {
  
        // 트렌젝션 시작
        mysqli_query($connect, "START TRANSACTION");
  
        // 쿼리들
  
        // 유저 정보 추가 쿼리 실행
        $sql = "INSERT INTO user_tb (user_id, best_star_count, ".
                "total_star_count) VALUES ('".$user_id."', 0, 0)";
        $result1 = mysqli_query($connect, $sql);
  
        // 비행기 타입1 구매 정보 추가 쿼리 실행
        $sql = "INSERT INTO plane_purchase_tb (user_id, plane_type, ".
                "is_purchase) VALUES ('".$user_id."', 'RED', TRUE)";
        $result2 = mysqli_query($connect, $sql);
  
        // 비행기 타입2 구매 정보 추가 쿼리 실행
        $sql = "INSERT INTO plane_purchase_tb (user_id, plane_type, ".
                "is_purchase) VALUES ('".$user_id."', 'GREEN', FALSE)";
        $result3 = mysqli_query($connect, $sql);
  
        // 비행기 타입2 구매 정보 추가 쿼리 실행
        $sql = "INSERT INTO plane_purchase_tb (user_id, plane_type, ".
                "is_purchase) VALUES ('".$user_id."', 'BLUE', FALSE)";
        $result4 = mysqli_query($connect, $sql);
  
        // 비행기 타입2 구매 정보 추가 쿼리 실행
        $sql = "INSERT INTO plane_purchase_tb (user_id, plane_type, ".
                "is_purchase) VALUES ('".$user_id."', 'YELLOW', FALSE)";
        $result5 = mysqli_query($connect, $sql);               
  
        if ($result1 && $result2 && $result3 && $result4 && $result5)
        {
            // 쿼리 적용
            mysqli_query($connect, "COMMIT"); // 쿼리를 적용함       
            $responseData["result_code"] = "LOGIN_SUCCESS";
  
            // 다시 유저 정보를 조회함
            find_user($connect, $user_id, $responseData);
        }
        else
        {
            mysqli_query($connect, "ROLLBACK"); // 쿼리 실행 이전상태로 돌림
            $responseData["result_code"] = "LOGIN_FAIL";
        }
    }
  
    // 유저 정보와 유저의 구매 정보를 json으로 변경하여 응답함 (Response)
    echo json_encode($responseData);
  
    mysqli_close($connect); // DB 연결 해제
 ?>
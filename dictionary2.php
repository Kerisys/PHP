<?php
	// 딕셔너리 생성2
    $monsterDic2 = array();
      
    $monsterDic2["name"] = "오우거";
    $monsterDic2["level"] = "5";
    $monsterDic2["hp"] = "100";
    $monsterDic2["weapons"] = array(
        "sword" => "빛나는 검",
        "gun" => "따발 총",
        "bow" => ""
    );

    echo json_encode($monsterDic2);
?>
<?php
	// array("Key" => value);

	$itemDic = array("name" => "번개검" , "price"=>300 );

    $monsterDic1 = array(
        "name" => "오우거",
        "level" => "5",
        "hp" => "100",
        "weapons" => array(
            "sword" => "빛나는 검",
            "gun" => "따발 총",
            "bow" => ""
        ),
        "complete_state" => array("stage1","stage2","stage3","stage4")
    );
//    $monsterDic1["weapons"] = $itemDic;

//    print_r($monsterDic1["weapons"]["sword"]);

    //print_r($monsterDic1["complete_state"]);

    // for($i=0;$i<count($monsterDic1["complete_state"]);$i++){
    // 	print_r($monsterDic1["complete_state"][$i]." ");
    // }

	echo json_encode($monsterDic1);
?>
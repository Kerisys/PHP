<?php
	$hp = 100;

	for($i=0;$i<3;$i++){
		$damage = mt_rand(5,30);
		$hp -= $damage;
	}

	echo "[HP : ".$hp."] => ";

	if($hp <= 30){
		echo "위험 상태";
	}else if($hp <= 60){
		echo "안전 상태";
	}else {
		echo "양호 상태";
	}

?>
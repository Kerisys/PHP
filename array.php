<?php
	$arr = array(10,20,30,40,50,60,70,80,90,100);

	print_r($arr);

	$arr[3] = 33;

	$arr[5] = "오십";

	for($i=0;$i<count($arr);$i++){
		echo "arr[".$i."]=>".$arr[$i]." ";
	}
	
?>
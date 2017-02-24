<?php

	function NoParamFunction(){
		echo 'NoParamFunction call, ';
	}
	function ParamFunction($param){
		echo 'ParamFunction call => param : '.$param.", ";
	}

	function ReturnParamFunction($param){
		return $param;
	}

	NoParamFunction();
	ParamFunction(10);
	$result = ReturnParamFunction("문자열");
	echo 'ReturnParamFunction call => return value : '.$result;

?>
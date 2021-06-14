<?php

	$username = $_POST["username"];
	$pwd = $_POST["pwd"];

	require_once 'dbh.inc.php';
	require_once 'functions.inc.php';

	if (emptyImputLogin($username, $pwd) !== false) {
		echo("1: Fill all the fields first!");
		exit();
	}

	loginUser($conn, $username, $pwd);
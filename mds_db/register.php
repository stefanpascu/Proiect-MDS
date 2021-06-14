<?php

	$firstname = $_POST["firstname"];
	$lastname = $_POST["lastname"];
	$username = $_POST["username"];
	$pwd = $_POST["pwd"];
	$repwd = $_POST["repwd"];

	require_once 'dbh.inc.php';
	require_once 'functions.inc.php';


	if (emptyImputSignup($firstname, $lastname, $username, $pwd, $repwd) !== false) {
		echo("1: Fill all the fields first!");
		exit();
	}

	if (invalidUser($username) !== false) {
		echo("2: Invalid Username!");
		exit();
	}

	if (pwdMatch($pwd, $repwd) !== false) {
		echo("3: The passwords don't match!");
		exit();
	}

	if (userExists($conn, $username) !== false) {
		echo ("4: Username already taken!");
		exit();
	}


	createUser($conn, $firstname, $lastname, $username, $pwd);

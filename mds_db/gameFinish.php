<?php

	$userId = $_POST["userId"];
	$username = $_POST["username"];

	require_once 'dbh.inc.php';
	require_once 'functions.inc.php';

	addGameFinish($conn, $userId, $username);
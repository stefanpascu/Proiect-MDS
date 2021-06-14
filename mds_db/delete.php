<?php

	$userId = $_POST["userId"];

	require_once 'dbh.inc.php';
	require_once 'functions.inc.php';

	deleteUser($conn, $userId);
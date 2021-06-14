<?php

function emptyImputSignup($firstname, $lastname, $username, $pwd, $repwd) {
 	$result;
 	if (empty($firstname) || empty($lastname) || empty($username) || empty($pwd) || empty($repwd)) {
 		$result = true;
 	}
 	else{
 		$result = false;
 	}
 	return $result;
 }

function invalidUser($username) {
 	$result;
 	if (!preg_match("/^[a-zA-Z0-9]*$/", $username)){
 		$result = true;
 	}
 	else{
 		$result = false;
 	}
 	return $result;
 }


 function pwdMatch($pwd, $repwd) {
 	$result;
 	if ($pwd !== $repwd){
 		$result = true;
 	}
 	else{
 		$result = false;
 	}
 	return $result;
 }

 function userExists($conn, $username) {
 	$sql = "SELECT * FROM players WHERE player_username = ?;";
 	$stmt = mysqli_stmt_init($conn);
 	if (!mysqli_stmt_prepare($stmt, $sql)) {
 		echo("5: Statement failed!");
 		exit();
 	}

 	mysqli_stmt_bind_param($stmt, "s", $username);
 	mysqli_stmt_execute($stmt);

 	$resultData = mysqli_stmt_get_result($stmt);

 	if ($row = mysqli_fetch_assoc($resultData)) {
 		return $row;
 	}
 	else{
 		$result = false;
 		return $result;
 	}

 	mysqli_stmt_close($stmt);
 }




 function createUser($conn, $firstname, $lastname, $username, $pwd) {
 	$sql = "INSERT INTO players (player_firstname, player_lastname, player_username, player_password) VALUES (?, ?, ?, ?);";
 	$stmt = mysqli_stmt_init($conn);
 	if (!mysqli_stmt_prepare($stmt, $sql)) {
 		echo("6: Statement failed!");
 		exit();
 	}

 	$hashedPwd = password_hash($pwd, PASSWORD_DEFAULT);

 	mysqli_stmt_bind_param($stmt, "ssss", $firstname, $lastname, $username, $hashedPwd);
 	mysqli_stmt_execute($stmt);

 	mysqli_stmt_close($stmt);

 	echo "0";
 	exit();

 }

function deleteUser($conn, $userId) {
	$sql = "DELETE FROM players WHERE player_id = $userId;";
	$stmt = mysqli_stmt_init($conn);
	if (!mysqli_stmt_prepare($stmt, $sql)) {
 		echo("7: Statement failed!");
 		exit();
 	}
 	mysqli_stmt_execute($stmt);
 	mysqli_stmt_close($stmt);
 	//header("location: ../login.php?error=none");
 	exit();
}



function emptyImputLogin($username, $pwd) {
 	$result;
 	if (empty($username) || empty($pwd)) {
 		$result = true;
 	}
 	else{
 		$result = false;
 	}
 	return $result;
 }

 function addGameStart($conn, $userId, $username){
 	$userExists = userExists($conn, $username, $username);
 	$sql = "UPDATE players SET games_started = ((SELECT games_started FROM players WHERE player_id = $userId) + 1) WHERE player_id = $userId;";
 	$stmt = mysqli_stmt_init($conn);
 	if (!mysqli_stmt_prepare($stmt, $sql)) {
 		echo("10: Statement failed!");
 		exit();
 	}
 	mysqli_stmt_execute($stmt);

 	mysqli_stmt_close($stmt);
 	echo "0\t" . $userExists["games_started"];
 	exit();
 }

 function addGameFinish($conn, $userId, $username){
 	$userExists = userExists($conn, $username, $username);
 	$sql = "UPDATE players SET games_finished = ((SELECT games_finished FROM players WHERE player_id = $userId) + 1) WHERE player_id = $userId;";
 	$stmt = mysqli_stmt_init($conn);
 	if (!mysqli_stmt_prepare($stmt, $sql)) {
 		echo("11: Statement failed!");
 		exit();
 	}
 	mysqli_stmt_execute($stmt);

 	mysqli_stmt_close($stmt);
 	echo "0\t" . $userExists["games_finished"];
 	exit();
 }


 function loginUser($conn, $username, $pwd){
 	$userExists = userExists($conn, $username, $username);
 	if ($userExists === false) {
 		echo("8: No user with this username!");
 		exit();
 	}

 	$pwdHashed = $userExists["player_password"];
 	$checkPwd = password_verify($pwd, $pwdHashed);

 	if ($checkPwd === false) {
 		echo("9: Wrong password!");
 		exit();
 	}
 	else if ($checkPwd === true) {
 		echo "0\t" . $userExists["player_id"] . "\t" . $userExists["player_firstname"] . "\t" . $userExists["player_lastname"] . "\t" . $userExists["player_username"] . "\t" . $userExists["games_started"] . "\t" . $userExists["games_finished"];
 		exit();
 	}
 }
<?php
require 'db.php';

$email = $_POST['emailPost'];
$password = $_POST['passwordPost'];

$sql = "SELECT userID, lastScene, role FROM user WHERE email = '$email' AND password = '$password'";
$result = mysqli_query($conn, $sql);

if (mysqli_num_rows($result) > 0) {
    $row = mysqli_fetch_assoc($result);

    $userID = $row['userID'];
    $lastScene = $row['lastScene'];
    $role = $row['role'];

    // If lastScene is null or empty, fallback to GameDemo
    if (empty($lastScene)) {
        $lastScene = "GameDemo";
    }

    // Return in format: userID|scene
    echo $userID . "|" . $lastScene . "|" . $role;
} else {
    echo "Invalid";
}
?>

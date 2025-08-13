<?php
require 'db.php';

$userID = $_POST['userID'];
$lastScene = $_POST['lastScene'];

$sql = "UPDATE user SET lastScene = '$lastScene' WHERE userID = '$userID'";

if (mysqli_query($conn, $sql)) {
    echo "success";
} else {
    echo "error: " . mysqli_error($conn);
}

mysqli_close($conn);
?>

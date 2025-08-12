<?php
require 'db.php';

$userID = $_POST['userID'];
$newScore = $_POST['newScore'];

$sql = "UPDATE user SET moralityScore = '$newScore' WHERE userID = '$userID'";
if (mysqli_query($conn, $sql)) {
    echo "Updated";
} else {
    echo "Error";
}
?>

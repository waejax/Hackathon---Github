<?php
require 'db.php';

$userID = $_POST['userID'];
$sql = "SELECT moralityScore FROM user WHERE userID = '$userID'";
$result = mysqli_query($conn, $sql);

if ($result && mysqli_num_rows($result) > 0) {
    $row = mysqli_fetch_assoc($result);
    echo $row['moralityScore'];
} else {
    echo "0";
}
?>

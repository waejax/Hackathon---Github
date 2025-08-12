<?php
    require 'db.php';

    $email = $_POST['emailPost'];
    $password = $_POST['passwordPost'];

    $sql = "SELECT lastScene FROM user WHERE email = '$email' AND password = '$password'";
    $result = mysqli_query($conn, $sql);

    if (mysqli_num_rows($result) > 0) {
        $row = mysqli_fetch_assoc($result);

        // If lastScene is null or empty, return PrimaryLevel
        if (empty($row['lastScene'])) {
            echo "GameDemo";
        } else {
            echo $row['lastScene'];
        }
    } else {
        echo "Invalid";
    }
?>

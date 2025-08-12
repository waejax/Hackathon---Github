<?php
    require 'db.php';

    $email = $_POST['emailPost'] ?? null;

    if (!$email) {
        die("No email provided.");
    }

    // Get lastScene for this user
    $sql = "SELECT lastScene FROM user WHERE email = '$email'";
    $result = mysqli_query($conn, $sql);

    if ($result && mysqli_num_rows($result) > 0) {
        $row = mysqli_fetch_assoc($result);
        $lastScene = $row['lastScene'];

        if (is_null($lastScene) || $lastScene === '') {
            // No last scene, go to PrimaryLevel
            header("Location: PrimaryLevel.php");
            exit;
        } else {
            // Has a last scene, go there
            header("Location: " . $lastScene . ".php");
            exit;
        }
    } else {
        echo "User not found.";
    }
?>
<?php
    require 'db.php';

    $username = $_POST["emailPost"];
    $password = $_POST["passwordPost"];
    $role = $_POST["rolePost"];

    $sql = "INSERT INTO user (email, password, role) VALUES ('".$username."','".$password."','".$role."')";
    $result = mysqli_query($conn, $sql);

    if(!$result) echo "there was an error";
    else echo "Everything ok.";

?>
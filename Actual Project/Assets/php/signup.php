<?php
    require 'db.php';

    $username = $_POST["emailPost"];
    $password = $_POST["passwordPost"];

    $sql = "INSERT INTO user (email, password) VALUES ('".$username."','".$password."')";
    $result = mysqli_query($conn, $sql);

    if(!$result) echo "there was an error";
    else echo "Everything ok.";

?>
<?php
    // db.php - Handles the database connection
    $servername = "localhost";
    $server_username = "root"; // Replace with your DB username
    $server_password = ""; // Replace with your DB password
    $dbname = "hackathon"; // Replace with your DB name

    // Create a new mysqli connection
    $conn = mysqli_connect($servername, $server_username, $server_password, $dbname);

    // Check the connection
    if (!$conn) {
        die("Connection failed: " . mysqli_connect_error());
    }
?>
<?php
error_reporting(E_ALL);
ini_set('display_errors', 1);

$host = "localhost";
$dbname = "hackathon";
$username = "root";
$password = "";

$conn = new mysqli($host, $username, $password, $dbname);

if ($conn->connect_error) {
    echo json_encode(["error" => "Connection failed: ".$conn->connect_error]);
    exit;
}

$sql = "SELECT line FROM primarylevel ORDER BY RAND() ASC LIMIT 1";
$result = $conn->query($sql);

$primary = array();

while ($row = $result->fetch_assoc()){
    $primary[] = $row['line'];
}

echo json_encode($primary);

$conn-> close();
?>
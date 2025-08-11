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

$sql = "SELECT line FROM demo ORDER BY id ASC";
$result = $conn->query($sql);

$contacts = array();

while ($row = $result->fetch_assoc()){
    $demos[] = $row['line'];
}

echo json_encode($demos);

$conn-> close();
?>
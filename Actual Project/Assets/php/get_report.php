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

$sql = "SELECT id, subject, incident, info, corruption, people, peopleAddress, position, peopleNo, peopleIc FROM report";
$result = $conn->query($sql);

$reports = array();

while ($row = $result->fetch_assoc()) {
    $reports[] = $row;
}

echo json_encode($reports);

$conn-> close();
?>
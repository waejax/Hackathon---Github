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

$sqlLeader = "SELECT userID, email, points, moralityScore FROM user WHERE role = 'user' ORDER BY moralityScore DESC";
$resultLeader = $conn->query($sqlLeader);

$leaders = array();

while ($row = $resultLeader->fetch_assoc()) {
    $leaders[] = $row;
}

$sqlStats = "SELECT count(*) as total,
    AVG(moralityScore) as avgScore
    from user WHERE role = 'user'";

$statsResult = $conn->query($sqlStats);
$stats = $statsResult->fetch_assoc();

$response = [
    "leaders" => $leaders,
    "stats" => $stats
];

echo json_encode($response);

$conn-> close();
?>
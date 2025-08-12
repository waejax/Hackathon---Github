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

$sql = "SELECT email, moralityScore FROM user ORDER BY moralityScore ASC";
$result = $conn->query($sql);

$leader = array();

while ($row = $result->fetch_assoc()){
    $leaders[] = $row[
        "email" => $row["email"],
        "score" => $row["score"]
    ];
}

echo json_encode($leaders);

$conn-> close();
?>
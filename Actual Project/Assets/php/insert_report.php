<?php
error_reporting(E_ALL);
ini_set('display_errors', 1);

$host = "localhost";
$dbname = "hackathon";
$username = "root";
$password = "";

$conn = new mysqli($host, $username, $password, $dbname);

if ($conn->connect_error) {
    die("Connection failed: ".$conn->connect_error);
}

file_put_contents("post_data.txt", print_r($_POST, true));

$subject = isset($_POST['subject']) ? $_POST['subject'] : "";
$incident = isset($_POST['incident']) ? $_POST['incident'] : "";
$people = isset($_POST['people']) ? $_POST['people'] : "";
$name = isset($_POST['name']) ? $_POST['name'] : "";
$email = isset($_POST['email']) ? $_POST['email'] : "";
$number = isset($_POST['number']) ? $_POST['number'] : "";
$ic = isset($_POST['ic']) ? $_POST['ic'] : "";

$subject = $conn->real_escape_string($subject);
$incident = $conn->real_escape_string($incident);
$people = $conn->real_escape_string($people);
$name = $conn->real_escape_string($name);
$email = $conn->real_escape_string($email);
$number = $conn->real_escape_string($number);
$ic = $conn->real_escape_string($ic);

$sql = "INSERT INTO report (subject, incident, people, repName, repEmail, repNo, repIC) VALUES
    ('$subject', '$incident', '$people', '$name', '$email', '$number', '$ic')";

if ($conn->query($sql) == TRUE) {
    echo "New record created successfully";
} else {
    echo "Error: ".$sql."<br>".$conn->error;
}

$conn-> close();
?>
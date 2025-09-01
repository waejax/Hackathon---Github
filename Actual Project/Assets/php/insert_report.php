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
//$people = isset($_POST['people']) ? $_POST['people'] : "";
$name = isset($_POST['name']) ? $_POST['name'] : "";
//$email = isset($_POST['email']) ? $_POST['email'] : "";
$number = isset($_POST['number']) ? $_POST['number'] : "";
$ic = isset($_POST['ic']) ? $_POST['ic'] : "";
$address = isset($_POST['address']) ? $_POST['address'] : "";
$position = isset($_POST['position']) ? $_POST['position'] : "";
$corruption = isset($_POST['type']) ? $_POST['type'] : "";
$info = isset($_POST['info']) ? $_POST['info'] : "";

$subject = $conn->real_escape_string($subject);
$incident = $conn->real_escape_string($incident);
//$people = $conn->real_escape_string($people);
$name = $conn->real_escape_string($name);
//$email = $conn->real_escape_string($email);
$number = $conn->real_escape_string($number);
$ic = $conn->real_escape_string($ic);
$address = $conn->real_escape_string($address);
$position = $conn->real_escape_string($position);
$corruption = $conn->real_escape_string($corruption);
$info = $conn->real_escape_string($info);

$uploadDir = "uploads/";
if (!is_dir($uploadDir)) {
    mkdir($uploadDir, 0777, true);
}

$storedPaths = [];
foreach ($_FILES as $key => $file)
{
    if (strpos($key, 'evidence') === 0 && $file['error'] == UPLOAD_ERR_OK)
    {
        $tmp = $file['tmp_name'];
        $orig = basename($file['name']);
        $newName = time() . "_" . uniqid() . "_" . $orig;
        $dest = $uploadDir . $newName;

        if (move_uploaded_file($tmp, $dest))
            $storedPaths[] = $dest;
    }
}

$evidenceField = $conn->real_escape_string(implode(',', $storedPaths));

$sql = "INSERT INTO report (subject, incident, info, corruption, people, peopleAddress, position, peopleNo, peopleIc, evidence) VALUES
    ('$subject', '$incident', '$info', '$corruption', '$name', '$address', '$position', '$number', '$ic', '$evidenceField')";

if ($conn->query($sql) == TRUE) {
    echo "New record created successfully";
} else {
    echo "Error: ".$sql."<br>".$conn->error;
}

$conn-> close();
?>
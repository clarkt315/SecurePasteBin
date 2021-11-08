<?php require 'header.php';

// Make sure that the values written to the database only include valid base64 characters.
// These are letters (upper and lower case), numbers, and /+=
$pattern = "@^[a-zA-Z0-9/+=]+$@";
if ((!preg_match($pattern, $_POST["pasteID"])) or 
    (!preg_match($pattern, $_POST["keySalt"])) or
    (!preg_match($pattern, $_POST["aesIV"])) or 
    (!preg_match($pattern, $_POST["cipherText"])) or
    (!preg_match($pattern, $_POST["macTag"])))
{
    echo "Invalid input characters.";
    exit(1);
}

// Depending on action variable, either do an insert or update.
if ($_POST["action"] == "insert")
{
    $pre_stmt = "INSERT INTO pastes VALUES (:pasteid, :keysalt, :aesiv, :ciphertext, :mactag)";
}
else if ($_POST["action"] == "update")
{
    $pre_stmt = "UPDATE pastes SET key_salt=:keysalt,aes_iv=:aesiv,cipher_text=:ciphertext,mac_tag=:mactag WHERE paste_id=:pasteid";
}
else
{
    echo "Invalid form action.";
    exit(1);
}

// Prepare sql and bind parameters.
$stmt = $conn->prepare($pre_stmt);
$stmt->bindParam(':pasteid', $_POST["pasteID"]);
$stmt->bindParam(':keysalt', $_POST["keySalt"]);
$stmt->bindParam(':aesiv', $_POST["aesIV"]);
$stmt->bindParam(':ciphertext', $_POST["cipherText"]);
$stmt->bindParam(':mactag', $_POST["macTag"]);
$stmt->execute();
$count = $stmt->rowCount();
echo $count;

$conn = null;
?>
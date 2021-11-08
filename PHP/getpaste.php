<?php require 'header.php';

// Prepare sql and bind parameters.
$stmt = $conn->prepare("SELECT key_salt, aes_iv, cipher_text, mac_tag FROM pastes WHERE paste_id =:pasteid");
$stmt->bindParam(':pasteid', $_POST["pasteID"]);
$stmt->execute();

// Only output results if 1 row is found.
if ($stmt->rowCount() == 1)
{
    $row = $stmt->fetch();
    $keySalt = $row['key_salt'];
    $aesIV = $row['aes_iv'];
    $cipherText = $row['cipher_text'];
    $macTag = $row['mac_tag'];
    // Make sure that the values returned from the database only include valid base64 characters.
    // These are letters (upper and lower case), numbers, and /+=
    // We also validate the data before inserting/updating the database (see savepaste.php),
    // but just in case somehow bad characters are in the database then we should not return them.
    $pattern = "@^[a-zA-Z0-9/+=]+$@";
    if ((!preg_match($pattern, $keySalt)) or
        (!preg_match($pattern, $aesIV)) or 
        (!preg_match($pattern, $cipherText)) or
        (!preg_match($pattern, $macTag)))
    {
        echo "Invalid input characters.";
        exit(1);
    }
    // If the above test passes, then return data.
    echo $keySalt;
    echo "|";
    echo $aesIV;
    echo "|";
    echo $cipherText;
    echo "|";
    echo $macTag;
}

$conn = null;
?>
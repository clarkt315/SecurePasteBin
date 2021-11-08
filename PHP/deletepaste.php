require 'header.php';

// Prepare sql and bind parameters.
$stmt = $conn->prepare("DELETE FROM pastes WHERE paste_id = :pasteid");
$stmt->bindParam(':pasteid', $_POST["pasteID"]);
$stmt->execute();
$count = $stmt->rowCount();
echo $count;

$conn = null;
?> 


<?php
$db_conn_config = parse_ini_file('../private/db_conn.ini');
$servername = $db_conn_config['servername'];
$username = $db_conn_config['username'];
$password = $db_conn_config['password'];
$dbname = $db_conn_config['dbname'];
$conn = new PDO("mysql:host=$servername;dbname=$dbname", $username, $password);
?>
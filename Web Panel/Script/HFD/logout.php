<?php
ob_start();
session_start();
include('connect.php');
setcookie("user");
setcookie("pass");
unset($_SESSION['count']);
unset($_SESSION['email']);
unset($_SESSION['ime']);
session_destroy();
?>

<!DOCTYPE html>
<html lang="en">
    <head>	
        <title>Logout | <?=$site_name; ?></title>
<?php
    include('inc/header.php');
?>
        
        <div class="container">
	<div class="row">
<?php 
        echo '<center><div class="text-success text-center"><strong>success</strong><br>
                   Logged out successfully.!
                </div></center>';
 echo"<meta http-equiv='Refresh' content='2; url=login.php'/>";
?>

        	</div>

</div>

   

<?php 
    include('inc/footer.php');

        ?>
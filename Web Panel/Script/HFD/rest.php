<?php
session_start();
include('connect.php');
$_SESSION['resetime'];
$mine = 10;
$duration = ($mine * 60);
$time = ($duration - (time() - $_SESSION['resetime']));
if($time <= 0)
    {
        unset($_SESSION['countreset']);
		unset($_SESSION['email']);
        unset($_SESSION['resetime']);
		unset($_SESSION['security_code']);
		session_destroy();
        header("Location: reset.php");
    }
if ($_SESSION['countreset'] <= 0){						
header('Location: reset.php');
}
else{
?>
<!DOCTYPE html>
<html lang="en">
    <head>	
        <title>Banned | <?=$site_name; ?></title>
<?php
    include('inc/header.php');
?>

  
<div class="container">
	<div class="row">
		<div class="col-md-4 col-md-offset-4 well">	
                          
        <div class="alert alert-error"><i class="fa fa-ban"></i><h4>Sorry!</h4>           
<b>You mistakeed in the correct reset password email entry 3 times.</b><br>
So you was banned from resetting password for 10 minutes and then <br>you can try to reset password again.
            </div>
        </div>
	</div>
</div>
        
<?php
include('inc/footer.php');
    } 
?>
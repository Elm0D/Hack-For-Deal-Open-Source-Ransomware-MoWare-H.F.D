<?php
ob_start(); 
session_start();
include('connect.php');
$functions = new functions();

@$co_username = $functions->save($functions->save3($_COOKIE['user']));
if (!isset($_SESSION['countreset'])) {
  $_SESSION['countreset'] = 0;
}
$_SESSION['resetime'] = time();
$mine = 10;
$duration = ($mine * 60);
$time = ($duration - (time() - $_SESSION['resetime']));
if($time <= 0){
    unset($_SESSION['countreset']);	
    unset($_SESSION['email']);
    unset($_SESSION['resetime']);
    session_destroy();
    header("Location:reset.php");
}
if ($_SESSION['countreset'] >= 3){						
    header('Location: rest.php');
}else{

?>
<!DOCTYPE html>
<html lang="en">
    <head>	
        <title>Reset Password | <?=$site_name; ?></title>
<?php
    include('inc/header.php');
?>
    
    
    

    
<?php
$error = '';

//send email
if (isset($_POST['send'])){
    $resetemail = $functions->save($functions->save3($_POST['resetemail']));
    if(!filter_var($resetemail,FILTER_VALIDATE_EMAIL)) {
		$email_error = "Please Enter Valid Email ID.!";
    }elseif(empty($_SESSION['security_code'] ) || strcasecmp($_SESSION['security_code'], $_POST['Captacha']) != 0){
        $captacha_error = 'The captcha code does not match!';
    }else{
                //`users`(`id`, `username`, `password`, `email`, `level`, `last_login`, `created_at`, `updated_at`, `last_ip`) 
        $query = mysql_query("SELECT * FROM $prefix.users WHERE email='$resetemail'") or die ("Query failed");
        $counts = mysql_num_rows($query);
        if($counts == 0){
            $_SESSION['countreset']++;
            $email_error = 'Incorrect Email address.!';
        }else{
            $rows = mysql_fetch_array($query);
            $encrypt = md5(1290*3+$rows['id']);
            $username2 = $functions->save($functions->save3($rows['username']));
            $useremail2 = $functions->save($functions->save3($rows['email']));
            if ($useremail2 == $resetemail){
                $to=$resetemail;
                $subject= $site_name." - New password activation";
                $from = $site_email;
                $body='<html><body style="text-align:left;"><h3>Password Reset</h3><span>Hello <b>'.$username2.'</b></span><br/>You are receiving this notification because you have (or someone pretending
to be you has) requested a new password be sent for your account on "'.$site_name.'".<br/><br/>If you did not request this notification then please
ignore it, if you keep receiving it please contact the board administrator.<br/><br/>
To use the new password you need to activate it. To do this click the link
provided below<br/><br/>
			 <a href="'.$site_url.'/reset.php?mode=activate&u='.$rows['id'].'&k='.$encrypt.'">'.$site_url.'/reset.php?mode=activate&u='.$rows['id'].'&k='.$encrypt.'</a><br/><br/>If successful you will be able to login using the following password:
<br/><br/>Password: '.$encrypt.'<br/><br/>You can of course change this password yourself via the profile page. If
you have any difficulties please contact the board administrator.<br/><br/><b>'.$site_name.'</b>Solve your problems.</body></html>';
                $headers = "From: " . strip_tags($site_name) . "\r\n";
                $headers .= "Reply-To: ". strip_tags($from) . "\r\n";
                $headers .= "MIME-Version: 1.0\r\n";
                $headers .= "Content-Type: text/html; charset=ISO-8859-1\r\n";
                $sendmail = mail($to,$subject,$body,$headers);
                if($sendmail){
                    $error = '<div class="text-success"><strong>Success</strong><br>A new password was sent to your registered email address.</div>';
                }else{
                    $error = '<div class="text-danger"><strong>Error</strong><br>Error sending, please try again later.</div>';
                }
            }else{
                $email_error = 'Incorrect Email address!';
            }
        }    
    }    
}



//reset pass
if (isset($_GET['mode'])){
    if($_GET['mode']=="activate"){
        $encrypt = $_GET['k'];
        $query = "SELECT id FROM $prefix.users where md5(1290*3+id)='".$encrypt."'";
        $result = mysql_query($query);
        $Results = mysql_fetch_array($result);
        if(count($Results)>=1){
            $query = "update $prefix.users set password='".md5($Results['id'])."' , updated_at=NOW() where id='".$Results['id']."'";
            $fff = mysql_query($query);
            if ($fff){
                $error = '<div class="text-success"><strong>Success</strong><br>Your new password has been activated.<br/><a href="login.php">Click here to login</a></div>';
            }
        }else{     
            $error = '<div class="texet-danger"><strong>Error</strong><br>Invalid key please try again..<br/><a href="'.$site_url.'/reset.php">Forget Password?</a></div>';       
        }
    }
}
    
?>

<div class="container">
	<div class="row">
		<div class="col-md-4 col-md-offset-4" style="margin-top: 90px;">
            <div class='panel panel-default'>
                <div class='panel-heading text-center'>
				    <a href="http://dev-plus.org/" title="Proudly Works With our program" target="_blank"><img src="img/Logo.png" alt="logo" /></a>
                    <h3 class='panel-title'><b>Mo</b>Ware <b>H</b>.<b>F</b>.<b>D</b> Reset Password</h3>
                </div>
                <div class='panel-body'>
<?php 
     
    if(!empty($error)){
        echo $error ;
    }else{
        echo "<span class='text-info'>Enter your email <strong>to reset your password</strong></strong></span>";    
    }
    if (isset($_GET['mode'])){
    }else{
 ?>
                    <form role="form" action="<?php echo $_SERVER['PHP_SELF']; ?>" method="post" name="resetform">
                        <fieldset>
					        <div class="form-group">
						        <label for="name">Email</label>
						        <input type="text" name="resetemail" placeholder="Email" required value="<?php if (isset($resetemail)) { echo $resetemail; } ?>" class="form-control" />
						        <span class="text-danger"><?php if (isset($email_error)) echo $email_error; ?></span>
					        </div>
							<div class="form-group">
								<label for="Captacha">Verification Code</label>
						        <div class="row">
								    <div class="col-md-6 col-sm-6">
						                <input type="text" name="Captacha" placeholder="Type Code" required class="form-control" />
										<span class="text-danger"><?php if (isset($captacha_error)) echo $captacha_error; ?></span>

									</div>
									<div class="col-md-6 col-sm-6">
									    <img src='lib/Capatcha/captcha_code_file.php?rand=<?php echo rand(); ?>' id="captchaimg" class="form-control pull-right" style="padding: 0;">
									</div>
						        </div>				           
					       </div>
							<div class="form-group">
                                <input type="submit" name="send" value="Send Email" class="btn btn-primary pull-right" />
					        </div>
						</fieldset>
			    </form>
<?php   
}
?>
				</div>
            
                <div class="panel-footer">
				    <div class="row">
				        <div class="col-md-6">
                            
						    <?php if (isset($co_username) && !empty($co_username)) { ?>
				                <a href="logout.php">Log Out</a>
				            <?php } else { ?>
				                <a href="login.php" >Login Now</a>
				           <?php } ?>
				        </div>
					    <div class="col-md-6">
						    <?php if (isset($co_username) && !empty($co_username)) { ?>
				                <a href="index.php" class="pull-right">Home</a>
				            <?php } ?>
				        </div>
					</div>
                </div>
		    </div>	
		</div>
	</div>

</div>   
<?php
include('inc/footer.php');
    } 
?>
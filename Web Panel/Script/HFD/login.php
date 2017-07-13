<?php
ob_start();
session_start();
include('connect.php');
$functions = new functions();

if (!isset($_SESSION['count'])) {
  $_SESSION['count'] = 0;
}


$_SESSION['ime'] = time();
$mine = 10;
$duration = ($mine * 60);
$time = ($duration - (time() - $_SESSION['ime']));
if($time <= 0)
    {
        unset($_SESSION['count']);
		unset($_SESSION['email']);
        unset($_SESSION['ime']);
		session_destroy();
        header("Location:login.php");
    }

if ($_SESSION['count'] >= 3){						
header('Location: logn.php');
}
else{
?>
<!DOCTYPE html>
<html lang="en">
    <head>	
        <title>Login | <?=$site_name; ?></title>
<?php
    include('inc/header.php');

$error = '';
     
     
 //login    
if (isset($_POST['login'])){
    $useremail =$functions->save($functions->save3($_POST['useremail']));
    $password = $functions->save($functions->save3($_POST['userpw']));
    $passwordm = md5($password);
    if(!filter_var($useremail,FILTER_VALIDATE_EMAIL)) {
		$email_error = "Please Enter Valid Email ID.!";
    }elseif(strlen($password) < 6) {
		$password_error = "Password must be minimum of 6 characters.!";
     }else{
        //`users`(`id`, `username`, `password`, `email`, `level`, `last_login`, `created_at`, `updated_at`, `last_ip`) 
        $query = mysql_query("SELECT * FROM $prefix.users WHERE email='$useremail' AND password='$passwordm'") or die ("Query failed");
        $counts = mysql_num_rows($query);
        if($counts == 0){
            $error = '<strong>Error</strong><br> Incorrect username or password.!<a href="reset.php"> Forgot your password ?</a>';
            $_SESSION['count']++;
    }else{
            $rows = mysql_fetch_array($query);  
            $username2 = $functions->save($functions->save3($rows['username']));
            $useremail2 = $functions->save($functions->save3($rows['email']));
            $password2 = $rows['password'];  
             if ($useremail2 == $useremail and $password2 == $passwordm){
                 if (isset($_POST['log_remb'])){
                     setcookie("user",$username2, time() + 60*60*24*365);
                     setcookie("pass",$passwordm, time() + 60*60*24*365);
                 }else{
                     setcookie("user",$username2);
                     setcookie("pass",$passwordm);
                 }
                 $_SESSION['email'] = $useremail2;

                 
                  echo '<div class="container"><div class="text-success text-center"><strong>You have successfully logged in.
</strong><br>
                    Welcome back <a href="#">'.$username2.'</a><br>
                    You Will be redirected to Home if not ? <a href="index.php">Click here</a> to continue .    
                </div></div>';
                 echo'<meta http-equiv="Refresh" content="5; url=index.php"  />';
                 exit;

             }else{
                 $error = '<strong>Error</strong><br> Incorrect username or password.!<a href="reset.php"> Forgot your password?</a>';
             }
        }
}
}else{

}
     ?>


<div class="container">
	<div class="row">
		<div class="col-md-4 col-md-offset-4" style="margin-top: 90px;">	
            <div class='panel panel-default'>
                <div class='panel-heading text-center'>
				    <a href="http://dev-plus.org/" target="_blank" title="Proudly Works With our program"><img src="img/Logo.png" alt="logo" /></a>
                    <h3 class='panel-title'><b>Mo</b>Ware <b>H</b>.<b>F</b>.<b>D</b> Login</h3>
                </div>
                <div class='panel-body'>
<?php 
     if(!empty($error)){
            echo '<span class="text-danger">'.$error.'</span>';
    }else{
        echo "<span class='text-info'>Enter your email and password <strong>to login</strong></span>";    
    }
?>
                    <form role="form" action="<?php echo $_SERVER['PHP_SELF']; ?>" method="post" name="loginform">
                        <fieldset>
                            <div class="form-group">
                                <label for="name">Email</label>
                                <input type="text" name="useremail" placeholder="Your Email" value="<?php if (isset($useremail)) { echo $useremail; } ?>" required class="form-control" />
						        <span class="text-danger"><?php if (isset($email_error)) echo $email_error; ?></span>
                            </div>
                            <div class="form-group">
						        <label for="name">Password</label>
						        <input type="password" name="userpw" placeholder="Your Password" value="<?php if (isset($password)) { echo $password; } ?>" required class="form-control" />
						        <span class="text-danger"><?php if (isset($password_error)) echo $password_error; ?></span>
					       </div>
						   <div class="form-group">
						        <div class="row">
                                    <div class="col-md-6 col-sm-6">
                                         <div class="checkbox">
                                             <label for="log_remb">
                                                 <input type="checkbox" id="log_remb" name="log_remb">Remember me
                                              </label>
                                        </div>
                                    </div>
									<div class="col-md-6 col-sm-6">
                                        <button type="submit" name="login" class="btn btn-primary pull-right"><i class="fa fa-sign-in"></i> Login</button>
									</div>
						        </div>				           
					       </div>
				    </fieldset>
			    </form>
				</div>
                <div class="panel-footer">
				    <div class="row">
				        <div class="col-md-12 col-sm-12">
					        <a href="reset.php" >I Lost my password ?</a>
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

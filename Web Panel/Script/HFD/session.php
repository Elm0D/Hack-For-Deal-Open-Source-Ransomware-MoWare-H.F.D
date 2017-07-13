<?php
ob_start(); 
session_start();
include('connect.php');
$functions = new functions();
$co_username = $functions->save($functions->save3($_COOKIE['user']));
$co_password = $functions->save($functions->save3($_COOKIE['pass']));
$session_id = $_SESSION['email'];
    if(isset($co_username) && !empty($co_username) && isset($co_password) && !empty($co_password) && isset($session_id) || !empty($session_id)) {
        $sqlll = mysql_query("select * from $prefix.users where username='$co_username' and email='$session_id' and password='$co_password'") or die ("Query failed");
        $nummm = mysql_num_rows($sqlll);
        if($nummm == 0){
            header("location: login.php");
            exit;
        }else{
            $r23 = mysql_fetch_assoc($sqlll);
            $user_id = $r23['id'];
            $user_name = $r23['username'];
            $level = $r23['level'];
            $ip = $functions->get_client_ip();
            $sql4=mysql_query("UPDATE $prefix.users SET last_login =NOW() , last_ip ='$ip' where id='$user_id'");  
          ###delte redirect to dash till of loop
        }
        
    }else{
        header("location: login.php");	
        exit;
    }	
?>
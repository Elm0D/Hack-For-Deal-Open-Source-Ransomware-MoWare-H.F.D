<?php
// Project    : MoWare Hack For Deal Script

//your databse hostname.
$dbhost = "localhost";
//your database username.
$dbuname = "root";
//your databse password
$dpass = "";
//your databse name.
$dbname = "moware_hfd";
//don't change unless you change this value in the db.
$prefix = "";

//change this
$site_name  = "MoWare H.F.D";
$site_email = "MoWare@MoWare.com";
$site_url  = "http://localhost/HFD/";	//be sure it must contain "/" at last of it
$split_stub = '--!-A-!--';

$bootstrapTheme = "default";
//$bootstrapTheme = "default";
//$bootstrapTheme = "cosmo";
//$bootstrapTheme = "darkly";
//$bootstrapTheme = "flatly";
//$bootstrapTheme = "lumen";
//$bootstrapTheme = "paper";
//$bootstrapTheme = "slate";
//$bootstrapTheme = "spacelab";
//$bootstrapTheme = "superhero";
//$bootstrapTheme = "yeti";

$connection = @mysql_connect($dbhost,$dbuname,$dpass) or die('<div align="center">هناك خطأ فى معلومات الإتصال ولم يتم الإتصال بالخادم</div>');
$database = mysql_select_db($dbname,$connection) or die('<div align="center">هناك خطأ فى معلومات قاعدة البيانات؟!</div>');
mysql_query("set names utf8_general_ci");
 

/*
	Copyright (C) 2017 dev-plus.org . All rights reserved.
*/
 ?>
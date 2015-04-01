<?php

$dir = "/Assets/Resources/MapFiles";
$dir = realpath(dirname(__FILE__)) . "/../../Assets/Resources/MapFiles";

$files = scandir($dir);

$mapFiles = array();

foreach($files as $file){
    if ( strpos($file, ".") != 0 && !strpos($file, ".meta") && strpos($file, ".json") > -1 ){
        array_push($mapFiles, $file);
    }
}

echo json_encode($mapFiles);

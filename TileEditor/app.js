var app = angular.module("TileMapEditor", []);

    app.factory("PrefabObject", ["", function () {

        var objectList = [
                "RedHead",
                "Spike",
                "Meat",
                "Key",
                "LionBoss",
                "FallingTile",
                "Boots"
            ];

        return {
            "nameToInteger": function (prefabObjectName) {
                var objectInteger = 0;
                switch(prefabObjectName){
                    default:
                    case "RedHead":
                        objectInteger = 0;
                        break;
                    case "Spike":
                        objectInteger = 1;
                        break;
                    case "Meat":
                        objectInteger = 2;
                        break;
                    case "Key":
                        objectInteger = 3;
                        break;
                    case "LionBoss":
                        objectInteger = 4;
                        break;
                    case "FallingTile":
                        objectInteger = 5;
                        break;
                    case "Boots":
                        objectInteger = 6;
                        break;
                }
                return objectInteger;
            },
            "integerToName": function (objectIndex) {
                return objectList[objectIndex];
            },
            "objectList": function () {
                return objectList;
            }
        };
    }]);

    app.controller("TileMapCtrl", ["$scope", "$http", function($scope, $http){

        /**
         * Sprite Sheet Information
         */

        $scope.tileWidth = 16;
        $scope.tileHeight = 16;
        $scope.spriteSheetWidth = 8;
        $scope.spriteSheetHeight = 4;

        $scope.displayScale = 3;
        $scope.gridScale = 4;

        $scope.getNumber = function(num) {
            return new Array(num);
        };

        /**
         * Map Data Related Information
         */

        $scope.map = {
            "name":   "Something Named Map",
            "width": 4,
            "height": 4
        };

        $scope.tileMapFG = {
            "data": [
                [ 0, 0, 0, 13 ],
                [ 13, 0, 0, 13 ],
                [ 5, 0, 21, 5 ],
                [ 2, 2, 2, 2 ]
            ]
        };

        $scope.tileMapDE = {
            "data": [
                [0, 0, 0, 0],
                [0, 0, 0, 0],
                [0, 0, 0, 0],
                [0, 0, 0, 0]
            ]
        };

        $scope.tileMapBG = {
            "data": [
                [16, 16, 16, 16],
                [16, 16, 16, 16],
                [16, 16, 16, 16],
                [16, 16, 16, 16]
            ]
        };

        $scope.doors = [];

        $scope.stuff = [];

        $scope.sizeChange = function (){
            $scope.tileMapFG.data = [];
            $scope.tileMapBG.data = [];
            $scope.tileMapDE.data = [];
            for( var y=0; y < $scope.map.height; y++){
                var tempRowFG = [],
                    tempRowDE = [],
                    tempRowBG = [];
                for( var x=0; x < $scope.map.width; x++){
                    tempRowFG.push(0);
                    tempRowDE.push(0);
                    tempRowBG.push(0);
                }
                $scope.tileMapFG.data.push(tempRowFG);
                $scope.tileMapDE.data.push(tempRowDE);
                $scope.tileMapBG.data.push(tempRowBG);
            }
        };

        /**
         * Painting Data
         */

        $scope.selected = "X";

        $scope.selectedXposition = null;
        $scope.selectedYposition = null;

        $scope.select = function (i){
            $scope.selected = i;
            $scope.selectedXposition = i % $scope.spriteSheetWidth;
            $scope.selectedYposition = $scope.spriteSheetHeight - 1 - Math.floor( i / $scope.spriteSheetWidth );
        };

        $scope.Math = window.Math;

        $scope.brushSize = 0;

        /**
         * this one is all 'forked up` - x & y are swapped
         * @param y
         * @param x
         * @param rel
         */
        $scope.paint = function(y, x, rel){

            if ( $scope.brushSize === "0" ) {
                if (rel === "bg") {
                    $scope.tileMapBG.data[x][y] = $scope.selected;
                }
                if (rel === "de") {
                    $scope.tileMapDE.data[x][y] = $scope.selected;
                }
                if (rel === "fg") {
                    $scope.tileMapFG.data[x][y] = $scope.selected;
                }
            } else {

                var minX = ( x - parseInt($scope.brushSize) >= 0 ) ? ( x - parseInt($scope.brushSize) ) : 0,
                    minY = ( y - parseInt($scope.brushSize) >= 0 ) ? ( y - parseInt($scope.brushSize) ) : 0,
                    maxX = ( x + parseInt($scope.brushSize) < $scope.map.width ) ? x + parseInt($scope.brushSize) : $scope.map.width - 1,
                    maxY = ( y + parseInt($scope.brushSize) < $scope.map.height ) ? y + parseInt($scope.brushSize) : $scope.map.height - 1;

                if (rel === "bg") {
                    for (var i = minY; i <= maxY; i++ ){
                        for(var j = minX; j <= maxX; j++ ){
                            $scope.tileMapBG.data[i][j] = $scope.selected;
                        }
                    }
                }
                if (rel === "de") {
                    for (var i = minY; i <= maxY; i++ ){
                        for(var j = minX; j <= maxX; j++ ){
                            $scope.tileMapDE.data[i][j] = $scope.selected;
                        }
                    }
                }
                if (rel === "fg") {
                    for (var i = minY; i <= maxY; i++ ){
                        for(var j = minX; j <= maxX; j++ ){
                            $scope.tileMapFG.data[i][j] = $scope.selected;
                        }
                    }
                }

            }
        };

        $scope.filePath = "/../Assets/Resources/MapFiles/";

        $scope.loadMap = function(fileName){
            $http.get($scope.filePath + fileName)
                .then(function(res){
                    console.log(res.data);
                    $scope.gridScale = 1;
                    $scope.map.name = res.data.name;
                    $scope.map.height = res.data.height;
                    $scope.map.width = res.data.width;
                    $scope.sizeChange();
                    $scope.tileMapFG.data = res.data.tileFG;
                    $scope.tileMapBG.data = res.data.tileBG;
                    $scope.tileMapDE.data = res.data.tileDE;
                    $scope.doors = res.data.doors;
                    $scope.stuff = res.data.stuff;
                });
        };

        //$scope.loadMap("a1_r1.json");

    }]);

    app.filter('commaNotLast', function(){
        return	function(input){
            return input == 1 ? '' : ',';
        };
    });


    app.directive('integer', function(){
        return {
            require: 'ngModel',
            link: function(scope, ele, attr, ctrl){
                ctrl.$parsers.unshift(function(viewValue){
                    return parseInt(viewValue, 10);
                });
            }
        };
    });
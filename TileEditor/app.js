var app = angular.module("TileMapEditor", []);

    app.controller("TileMapCtrl", ["$scope", "$http", function($scope, $http){

        /**
         * Sprite Sheet Information
         */

        $scope.tileWidth = 16;
        $scope.tileHeight = 16;
        $scope.spriteSheetWidth = 8;
        $scope.spriteSheetHeight = 4;

        $scope.displayScale = 2;
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
                [ 0,  0,  0, 13 ],
                [ 13, 0,  0, 13 ],
                [ 5,  0, 21,  5 ],
                [ 2,  2,  2,  2 ]
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

        $scope.prefabObjects = [
            "RedHead",      // 0
            "Spike",
            "Meat",
            "Key",
            "LionBoss",
            "FallingTile",
            "Boots"
        ];

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
         * PAINTING MODULE
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


        /**
         * LOADING MODULE
         * @type {string}
         */

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

        /**
         * HANDLING STUFF
         */

        $scope.thingTypeIndex = null;

        $scope.activeCell = null;

        $scope.displayCellDetail = function(x,y){

            var thing = null,
                thingIndex = null,
                door = null,
                doorIndex = null;

            // iterate through doors and identify if this cell is occupied
            for(var s=0; s<$scope.stuff.length; s++){
                if ( $scope.stuff[s].xPosition == x && $scope.stuff[s].yPosition == y  ){
                    thing = $scope.stuff[s];
                    thingIndex = s;
                }
            }

            // iterate through stuff and identify if this cell is occupied
            for(var d=0; d<$scope.doors.length; d++){
                if ( $scope.doors[d].xPosition == x && $scope.doors[d].yPosition == y  ){
                    door = $scope.doors[d];
                    doorIndex = d;
                }
            }

            // display a modal window with cell information
            $scope.activeCell = {
                "x": x,
                "y": y,
                "thing": thing,
                "thingIndex": thingIndex,
                "door": door,
                "doorIndex": doorIndex
            };

            if ( thing.type > -1 )
                $scope.thingTypeIndex = thing.type;

            // give ability to modify / delete or add stuff / doors from drop down menu

        };

        // close the active cell tab
        $scope.deactivateCell = function(){
            $scope.activeCell = null;
            $scope.thingTypeIndex = null;
        };

        $scope.deleteContents = function(x, y){
            if ( $scope.activeCell != null ){
                if ( $scope.activeCell.thingIndex !== null && $scope.activeCell.thingIndex > -1 ){
                    if ( $scope.stuff[$scope.activeCell.thingIndex].xPosition == x && $scope.stuff[$scope.activeCell.thingIndex].yPosition == y ){
                        $scope.stuff.splice($scope.activeCell.thingIndex, 1);
                    }
                }

                if ( $scope.activeCell.doorIndex !== null && $scope.activeCell.doorIndex > -1){
                    if ( $scope.doors[$scope.activeCell.doorIndex].xPosition == x && $scope.doors[$scope.activeCell.doorIndex].yPosition == y ){
                        $scope.doors.splice($scope.activeCell.doorIndex, 1);
                    }
                }
                $scope.deactivateCell();
            }
        };

        $scope.saveContents = function(x, y){
            if ( $scope.activeCell != null ){
                // check for element by XY

                var thing = null,
                    thingIndex = null,
                    door = null,
                    doorIndex = null;

                // iterate through doors and identify if this cell is occupied
                for(var s=0; s<$scope.stuff.length; s++){
                    if ( $scope.stuff[s].xPosition == x && $scope.stuff[s].yPosition == y  ){
                        thing = $scope.stuff[s];
                        thingIndex = s;
                    }
                }

                // iterate through stuff and identify if this cell is occupied
                for(var d=0; d<$scope.doors.length; d++){
                    if ( $scope.doors[d].xPosition == x && $scope.doors[d].yPosition == y  ){
                        door = $scope.doors[d];
                        doorIndex = d;
                    }
                }

                // if NOT EMPTY update the data
                if ( thing != null || door != null ){
                    if ( thing != null ){
                        $scope.stuff[thingIndex] = $scope.activeCell.thing;
                    } else if ( door != null ){
                        $scope.doors[doorIndex] = $scope.activeCell.door;
                    }
                } else { // ELSE ADD NEW ELEMENT
                    if ( $scope.activeCell.thing != null ){
                        $scope.activeCell.thing.xPosition = x;
                        $scope.activeCell.thing.yPosition = y;
                        $scope.stuff.push($scope.activeCell.thing);
                    } else if ( $scope.activeCell.door != null ){
                        $scope.activeCell.door.xPosition = x;
                        $scope.activeCell.door.yPosition = y;
                        $scope.stuff.push($scope.activeCell.door);
                    }
                }

            }
        };

        $scope.updateActiveCellThingType = function(indexValue){
            if ( $scope.activeCell.thing == null ){
                $scope.activeCell.thing = {
                        "type": indexValue,
                        "xPosition": null,
                        "yPosition": null,
                        "objectName": null,
                        "r": null,
                        "g": null,
                        "b": null
                    };
            } else {
                $scope.activeCell.thing.type = indexValue;
            }
        };


    }]);

    // if its not the last element in the array, add a comma after it
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
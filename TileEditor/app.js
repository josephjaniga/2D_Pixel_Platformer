var app = angular.module("TileMapEditor", []);

    app.controller("TileMapCtrl", [ "$scope", function($scope){

        /**
         * Sprite Sheet Information
         */

        $scope.tileWidth = 16;
        $scope.tileHeight = 16;
        $scope.spriteSheetWidth = 8;
        $scope.spriteSheetHeight = 3;

        $scope.displayScale = 3;
        $scope.gridScale = 1.5;

        $scope.getNumber = function(num) {
            return new Array(num);
        };

        /**
         * Map Data Related Information
         */

        $scope.mapName = "Something Named Map";
        $scope.mapWidth = 4;
        $scope.mapHeight = 4;

        $scope.tileMapFG = [
            [ 0, 0, 0, 13 ],
            [ 13, 0, 0, 13 ],
            [ 5, 0, 21, 5 ],
            [ 2, 2, 2, 2 ]
        ];

        $scope.tileMapDE = [
            [ 0, 0, 0, 0 ],
            [ 0, 0, 0, 0 ],
            [ 0, 0, 0, 0 ],
            [ 0, 0, 0, 0 ]
        ];

        $scope.tileMapBG = [
            [ 16, 16, 16, 16 ],
            [ 16, 16, 16, 16 ],
            [ 16, 16, 16, 16 ],
            [ 16, 16, 16, 16 ]
        ];

        $scope.sizeChange = function (){
            $scope.tileMapFG = [];
            $scope.tileMapBG = [];
            $scope.tileMapDE = [];
            for( var y=0; y < $scope.mapHeight; y++){
                var tempRowFG = [],
                    tempRowDE = [],
                    tempRowBG = [];
                for( var x=0; x < $scope.mapWidth; x++){
                    tempRowFG.push(0);
                    tempRowDE.push(0);
                    tempRowBG.push(0);
                }
                $scope.tileMapFG.push(tempRowFG);
                $scope.tileMapDE.push(tempRowDE);
                $scope.tileMapBG.push(tempRowBG);
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

        $scope.paint = function(x, y, rel){
            if ( rel === "bg" ) { $scope.tileMapBG[x][y] = $scope.selected; }
            if ( rel === "de" ) { $scope.tileMapDE[x][y] = $scope.selected; }
            if ( rel === "fg" ) { $scope.tileMapFG[x][y] = $scope.selected; }
        };


    }]);

    app.filter('commaNotLast', function(){
        return	function(input){
            return input == 1 ? '' : ',';
        };
    });
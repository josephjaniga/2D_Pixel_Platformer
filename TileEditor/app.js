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
        $scope.gridScale = 3;

        $scope.getNumber = function(num) {
            return new Array(num);
        }

        /**
         * Map Data Related Information
         */

        $scope.mapName = "Something Named Map";
        $scope.mapWidth = 4;
        $scope.mapHeight = 4;

        $scope.tileMapFG = [
            [ 0, 0, 0, 0 ],
            [ 0, 0, 0, 0 ],
            [ 0, 0, 0, 0 ],
            [ 0, 0, 0, 9 ]
        ];

        $scope.tileMapBG = [
            [ 0, 0, 0, 0 ],
            [ 0, 0, 0, 0 ],
            [ 0, 0, 0, 0 ],
            [ 0, 0, 0, 0 ]
        ];

        $scope.sizeChange = function (){
            $scope.tileMapFG = [];
            $scope.tileMapBG = [];
            for( var y=0; y < $scope.mapHeight; y++){
                var tempRow = [];
                for( var x=0; x < $scope.mapWidth; x++){
                    tempRow.push(0);
                }
                $scope.tileMapFG.push(tempRow);
                $scope.tileMapBG.push(tempRow);
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
            if ( rel.toLowerCase() === "bg")
                $scope.tileMapBG[x][y] = $scope.selected;
            else
                $scope.tileMapFG[x][y] = $scope.selected;
        };


    }]);

    app.filter('commaNotLast', function(){
        return	function(input){
            return input == 1 ? '' : ',';
        };
    });
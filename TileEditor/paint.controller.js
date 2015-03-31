
angular
    .module("TileMapEditor.paintController")
    .controller('paintController', function(){


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



    });

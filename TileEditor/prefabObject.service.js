angular.factory("PrefabObject", ["", function () {

    /**
     * PREFAB OBJECT SERVICE
     */

    var objectList = [
        "RedHead",      // 0
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
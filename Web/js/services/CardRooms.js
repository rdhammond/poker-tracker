angular.module('pokerTracker.services', [])
    .factory('CardRooms', ['$http', '$q', 'urlActions', function ($http, $q, urlActions) {
        return {
            get: function () {
                return $q(function (resolve, reject) {
                    $http.get(urlActions.getCardRooms).success(resolve);
                });
            }
        };
    }
]);
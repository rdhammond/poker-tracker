angular.module('pokerTracker.services', [])
    .factory('GameTypes', ['$http', '$q', 'urlActions', function($http, $q, urlActions) {
        return {
            get: function () {
                return $q(function (resolve, reject) {
                    $http.get(urlActions.getGameTypes).success(resolve);
                });
            }
        };
    }
]);
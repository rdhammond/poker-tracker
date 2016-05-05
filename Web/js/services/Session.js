angular.module('pokerTracker.services', [])
    .factory('Session', ['$http', '$q', 'urlActions', function($http, $q, urlActions) {
        return {
            create: function () {
                return $q(function (reject, resolve) {
                    $http.get(urlActions.createSession).success(function (session) {
                        session.StartingStackSize = null;
                        resolve(session);
                    });
                });
            }
        }
    }
]);
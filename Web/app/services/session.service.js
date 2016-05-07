(function () {
    'use strict';

    angular
        .module('app')
        .service('session', session);

    session.$inject = ['$http', urlActions];

    function session($http, urlActions) {
        return { save: save };

        function save(sessionInfo) {
            return $http.post(urlActions.saveSession, sessionInfo);
        }
    }
})();
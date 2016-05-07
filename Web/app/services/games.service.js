(function () {
    'use strict';

    angular
        .module('app')
        .service('gameTypes', gameTypes);

    gameTypes.$inject = ['$http', 'urlActions'];

    function gameTypes($http, urlActions) {
        return { get: get };

        function get() {
            return $http.get(urlActions.getGameTypes);
        }
    }
})();
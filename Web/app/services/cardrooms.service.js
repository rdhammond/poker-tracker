(function () {
    'use strict';

    angular
        .module('app')
        .service('cardRooms', cardRooms);

    cardRooms.$inject = ['$http', 'urlActions'];

    function cardRooms($http, urlActions) {
        return { get: get };

        function get() {
            return $http
                .get(urlActions.getCardRooms)
                .then(function (response) { return response.data; });
        }
    }
})();
(function () {
    'use strict';

    angular
        .module('app')
        .service('msDate', msDate);

    function msDate() {
        return { from: from };

        function from(date) {
            return '/Date(' + date.getTime() + ')/';
        }
    }
})();
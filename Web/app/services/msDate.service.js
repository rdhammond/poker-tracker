(function () {
    'use strict';

    angular
        .module('app')
        .service('msDate', msDate);

    function msDate() {
        return { now: now };

        function now() {
            return '/Date(' + Date.now.getTime() + ')/';
        }
    }
})();